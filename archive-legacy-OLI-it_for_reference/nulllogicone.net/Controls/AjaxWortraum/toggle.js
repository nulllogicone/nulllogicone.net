function ToggleKnoten(item, onlymarked)
{
    // Child-div neu füllen
	var cdiv=document.getElementById(item);
	var mm = AjaxWortraumControl.IsMarkierbar().value;
	if(cdiv.style.display == "none"){
		if(mm || cdiv.innerHTML == "" || onlymarked == "True"){
			cdiv.innerHTML=AjaxWortraumControl.MakeWeiterKnoten("" + item + "","False").value;
			// Knoten aktualisieren
			var kdiv=document.getElementById("K" + item);
			kdiv.innerHTML=AjaxWortraumControl.MakeKnoten("" + item + "").value;  
		}
		if(cdiv.innerHTML != "") {
			cdiv.style.display = "block";
		}
	}
	else {
		cdiv.style.display = "none";
	}

}

function ToggleZweig(lastknoten, item, onlymarked)
{
	// Child-div neu füllen
	var cdiv=document.getElementById(lastknoten + item);
	var mm = AjaxWortraumControl.IsMarkierbar().value;
	if(cdiv.style.display == "none" ){
		if(mm || cdiv.innerHTML == "" || onlymarked == "True"){
			cdiv.innerHTML=AjaxWortraumControl.MakeWeiterZweig("" + lastknoten + "","" + item + "","False").value;
			// Zweig aktualisieren
			var zdiv=document.getElementById("Z" + lastknoten + item);
			zdiv.innerHTML=AjaxWortraumControl.MakeZweig("" + lastknoten + "","" + item + "").value;  
		}
		if(cdiv.innerHTML != "") {
			cdiv.style.display = "block";
		}
	}
	else {
		cdiv.style.display = "none";
	}
	

}


function ClearKnoten(item) 
{
	AjaxWortraumControl.ClearKnoten("" + item + "");
		// Knoten aktualisieren
    var kdiv=document.getElementById("K" + item);
    kdiv.innerHTML=AjaxWortraumControl.MakeKnoten("" + item + "").value; 
}
	
function ClearZweig(kitem, zitem) 
{
//	var childdiv = document.getElementById(kitem + zitem);
//	if (childdiv.style.display == "none") {
		AjaxWortraumControl.ClearZweig("" + kitem + "","" + zitem + "");
		// Knoten aktualisieren
		var zdiv=document.getElementById("Z" + kitem + zitem);
		zdiv.innerHTML=AjaxWortraumControl.MakeZweig("" + kitem + "","" + zitem + "").value; 
//	} 
}

function Edit(item)
{
	var div = document.getElementById(item);
	if(div.style.display == "none"){
		div.style.display = "inline";
	} else {
		div.style.display = "none";
	}
}

function KnotenUpdate(item)
{
	var str = new String(item.parentElement.id);
	kguid = str.substring(4);
	void AjaxWortraumControl.UpdateKnoten(kguid, item.value);
	
	var div = item.parentElement;
	div.style.display = "none";	
	
	var kdiv = document.getElementById("K" + kguid);
	kdiv.innerHTML = AjaxWortraumControl.MakeKnoten("" + kguid + "").value;
	

}

function ZweigUpdate(item)
{
	var str = new String(item.parentElement.id);
	kguid = str.substring(4,40);
	zguid = str.substring(40);
	void AjaxWortraumControl.UpdateZweig(kguid, zguid, item.value);
	
	var div = item.parentElement;
	div.style.display = "none";	
	
	var zdiv = document.getElementById("Z" + kguid + zguid);
	zdiv.innerHTML = AjaxWortraumControl.MakeZweig("" + kguid + "","" + zguid + "").value;
}