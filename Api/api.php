<?php
	//Tabulky :	uzivatele (id AI, jmeno, helso)
	//			zbozi (id AI, nazev)
	//			obednavka (id AI, id_obednavky, id_zbozi, mozstvi)
	//			obednavky (id AI, id_majitele, datum, stav)


	// Spojení s databazí
	$mysqli = new mysqli('127.0.0.1', 'bednaja14', 'Magic007', 'bednaja14', 0,'/var/run/mysqld/mysqld.sock');

	// Co se bude dít
	$action = isset( $_GET['action'] ) ? $_GET['action'] : "";
	// V jaký tabulce se to bude dít
	$table = isset( $_GET['table'] ) ? $_GET['table'] : "";	

	//id položky s kterou pracuju
	$get_id = isset( $_GET['id'] ) ? $_GET['id'] : "";
	//id nebo jmeno zakazníka
	$get_jmeno = isset( $_GET['jmeno'] ) ? $_GET['jmeno'] : "";
	//heslo uzivatele
	$get_heslo = isset( $_GET['heslo']) ? $_GET['heslo'] : "";

	switch( $action ){

		case 'select':
			select_all($mysqli, $table);
		break;

		case 'login':
			login($mysqli, $get_jmeno, $get_heslo);
		break;

		case 'add':
			add($mysqli, $get_id, $get_jmeno);
		break;

		case 'cart':
			cart($mysqli, $get_jmeno, $table);
		break;

		case 'buy':
			buy($mysqli, $get_jmeno, $table);
		break;

		case 'reset_password':
			reset_password($mysqli, $get_jmeno, $get_heslo, "Json-api");
		break;	

		case 'change_name':
			change_name($mysqli, $get_jmeno, $get_id);
		break;		

//nefunguje
		case 'delete':
			delete($get_id); 
		break;

		case 'last_date':
			last_date($mysqli, $get_jmeno); 
		break;

		case 'obednavky':
			obednavky($mysqli, $get_jmeno); 
		break;

		case 'obednavka':
			obednavka($mysqli, $get_id); 
		break;
 	}


    	//Výpis všeho!
 			//?action=select&table=zbozi
 			//vrací json se všemy položkamy ze zboží
 		function select_all($mysqli, $table) {
			$vysledek = $mysqli->query("SELECT * FROM `".$table."`");
			$rows = array();
			while($r = mysqli_fetch_assoc($vysledek)) {
			    $rows[] = $r;
			}
			print json_encode($rows);
			$mysqli->close();		 		
 		}

 		//Kontrola přihlašeni
 			//?action=login&jmeno="derp"&heslo="kreslo"
 			//vrací objekt osoba
 		function login($mysqli, $get_jmeno, $get_heslo) {
		 	$vysledek = $mysqli->query("SELECT * FROM `Json-api` WHERE `jmeno`=\"".$get_jmeno."\" && `heslo`=\"".$get_heslo."\"");
			$rows = array();
			$r = mysqli_fetch_assoc($vysledek);  
			print json_encode($r);
			$mysqli->close();
 		}

 		//Přidání položky do obednávky když existuje nebo jí vytvoří novou obednávku a pak jí uloží
 			//?action=add&id="11"&jmeno="6" 11=id zboží 6=id uživatele
 			//vrací OK
		function add($mysqli, $get_id, $get_jmeno) {

 			$obednavka = get_id_obednavka($mysqli, $get_jmeno);

 			$vysledek = $mysqli->query("SELECT * FROM `obednavka` WHERE `id_obednavky` = \"".$obednavka."\" and `id_zbozi` = \"".$get_id."\"");
			$r = mysqli_fetch_assoc($vysledek);
			if ($r != null){
				
				$mnoz = $r['mnozstvi']+1;
				$sql = "UPDATE `obednavka` SET `mnozstvi`=".$mnoz." WHERE `id`=".$r['id'];
				$mysqli->query($sql);
				print("UPDATE");
			}else{
				echo "INSERT INTO `obednavka` (`id_obednavky`, `id_zbozi`, `mnozstvi`) VALUES (".$obednavka.", ".$get_id.",1)";
				$mysqli->query("INSERT INTO `obednavka` (`id_obednavky`, `id_zbozi`, `mnozstvi`) VALUES (".$obednavka.", ".$get_id.",1)");
				print("OK");
			}
 			$mysqli->close();
 		}

 			//vratí id obednavky podle jmena
 		function get_id_obednavka($mysqli, $get_jmeno) {
 			$vysledek = $mysqli->query("SELECT * FROM `obednavky` WHERE `id_majitele` = \"".$get_jmeno."\" and `stav` = 0");
			$r = mysqli_fetch_assoc($vysledek);
			if ($r == null){
				
				$mysqli->query("INSERT INTO `obednavky` (`id_majitele`, `stav`) VALUES (".$get_jmeno.", 0)");
				return $mysqli->insert_id;
			}else{
				return $r['id'];
			}
			$mysqli->close();
 		}

 		//Vypis aktualní obednávky "košík"
 			// ?action=cart&table=obednavka&jmeno="6" 6=id uzivatele
 			// vrátí json se všemy nekoupenymy položkamy uživatele
 		function cart($mysqli, $get_jmeno, $table){
			$vysledek = $mysqli->query("SELECT * FROM `".$table."`WHERE `id_obednavky` = ".get_id_obednavka($mysqli, $get_jmeno));
			//echo "SELECT * FROM `".$table." `WHERE `id_obednavky` = ".get_id_obednavka($get_jmeno);
			$rows = array();
			while($r = mysqli_fetch_assoc($vysledek)) {
				//echo $r['id_zbozi'];
				$vysledek2 = $mysqli->query("SELECT * FROM `zbozi` WHERE `id` = ".$r['id_zbozi']);
				$r2 = mysqli_fetch_assoc($vysledek2);
			    $rows[] = $r2;
			}
			print json_encode($rows);
			$mysqli->close();	
 		}

 		//Změní heslo uživatele
 			// ?action=buy&table=obednavka&jmeno="6" 6= id zakaznika
 			// vrátí "OK"
 		function reset_password($mysqli, $get_jmeno, $get_heslo, $table){
			$vysledek = $mysqli->query("UPDATE `".$table."` SET  `heslo` = '".$get_heslo."' WHERE `jmeno` = ".$get_jmeno);
			print("OK");
			$mysqli->close();	
 		}

 		//Změní položky v košíku v zakoupené 
 			// ?action=buy&table=obednavka&jmeno="6" 6= id zakaznika
 			// vrátí "OK"
 		function buy($mysqli, $get_jmeno, $table){
			$vysledek = $mysqli->query("UPDATE `obednavky` SET  `stav` =  '1' WHERE  `obednavky`.`id_majitele` = ".$get_jmeno);
			print("OK");
			$mysqli->close();	
 		}
 		

 		function change_name($mysqli, $get_jmeno, $get_id){
			$vysledek = $mysqli->query("UPDATE `Json-api` SET  `jmeno` =  '".$get_jmeno."' WHERE  `id` = ".$get_id);
			print("OK");
			$mysqli->close();	
 		}

		//Delete záznamu v DB podle ID
 		//
		function delete($mysqli, $id, $table){
			
			$mysqli->query("DELETE FROM `".$table."` WHERE `id` =".$id);
			echo "OK";
			$mysqli->close();
		}

		function last_date($mysqli, $jmeno){
			$output = $mysqli->query("SELECT SUM( id ) + SUM( stav ) FROM  `obednavky` WHERE  `id_majitele` =".$jmeno); 
			if ($output->num_rows > 0) {
			    // output data of each row
			    while($row = $output->fetch_assoc()) {
			        echo $row["SUM( id ) + SUM( stav )"];
			    }
			} else {
			    echo 0;
			}
			
			$mysqli->close();
		}

		function obednavky($mysqli, $get_jmeno) {
		 	$vysledek = $mysqli->query("SELECT `id`,`datum`,`nazev` FROM `obednavky` WHERE `id_majitele`=\"".$get_jmeno."\"");
			$rows = array();

			$rows = array();
			while($r = mysqli_fetch_assoc($vysledek)) {
			    $rows[] = $r;
			}
			print json_encode($rows);
			$mysqli->close();
 		}

 		function obednavka($mysqli, $get_id) {
		 	$vysledek = $mysqli->query("SELECT `id_obednavky` ,  `mnozstvi` ,  `zbozi`.`nazev` 
		 		FROM `obednavka` 
				JOIN  `zbozi` ON  `id_zbozi` =  `zbozi`.`id` 
				WHERE  `id_obednavky` = ".$get_id);

			$rows = array();
			while($r = mysqli_fetch_assoc($vysledek)) {
			    $rows[] = $r;
			}
			print json_encode($rows);
			$mysqli->close();
 		}

?>