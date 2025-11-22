<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AjaxWortraumControlFlip.ascx.cs" Inherits="nulllogicone.net.Controls.AjaxWortraum.AjaxWortraumControlFlip" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<style>
a {text-decoration:none;}
.netz { COLOR: navy }
A.knoten { COLOR: black; TEXT-DECORATION: none }
A.knoten:hover { BACKGROUND-COLOR: #dee8ff }
A.knoten:visited { COLOR: black }
.baum { COLOR: darkgreen }
A.zweig { COLOR: black; TEXT-DECORATION: none }
A.zweig:hover { BACKGROUND-COLOR: #ddffdd }
A.zweig:visited { COLOR: black }
</style>
<asp:Panel id="WortraumPanel" runat="server">
	<asp:label id="WortraumLabel" runat="server"></asp:label>
</asp:Panel>
<!-- script src="toggle.js" type="text/javascript"> /script> -->
<script language="javascript">
function ToggleKnoten(item, onlymarked)
{
    // Child-div neu füllen
	var cdiv=document.getElementById(item);
	var mm = AjaxWortraumControlFlip.IsMarkierbar().value;
	if(cdiv.style.display == "none"){
		if(mm || cdiv.innerHTML == "" || onlymarked == "True"){
			cdiv.innerHTML=AjaxWortraumControlFlip.MakeWeiterKnoten("" + item + "","False").value;
			// Knoten aktualisieren
			var kdiv=document.getElementById("K" + item);
			kdiv.innerHTML=AjaxWortraumControlFlip.MakeKnoten("" + item + "").value;  
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
	var mm = AjaxWortraumControlFlip.IsMarkierbar().value;
	if(cdiv.style.display == "none" ){
		if(mm || cdiv.innerHTML == "" || onlymarked == "True"){
			cdiv.innerHTML=AjaxWortraumControlFlip.MakeWeiterZweig("" + lastknoten + "","" + item + "","False").value;
			// Zweig aktualisieren
			var zdiv=document.getElementById("Z" + lastknoten + item);
			zdiv.innerHTML=AjaxWortraumControlFlip.MakeZweig("" + lastknoten + "","" + item + "").value;  
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
	AjaxWortraumControlFlip.ClearKnoten("" + item + "");
		// Knoten aktualisieren
    var kdiv=document.getElementById("K" + item);
    kdiv.innerHTML=AjaxWortraumControlFlip.MakeKnoten("" + item + "").value; 
}
	
function ClearZweig(kitem, zitem) 
{
//	var childdiv = document.getElementById(kitem + zitem);
//	if (childdiv.style.display == "none") {
		AjaxWortraumControlFlip.ClearZweig("" + kitem + "","" + zitem + "");
		// Knoten aktualisieren
		var zdiv=document.getElementById("Z" + kitem + zitem);
		zdiv.innerHTML=AjaxWortraumControlFlip.MakeZweig("" + kitem + "","" + zitem + "").value; 
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
	void AjaxWortraumControlFlip.UpdateKnoten(kguid, item.value);
	
	var div = item.parentElement;
	div.style.display = "none";	
	
	var kdiv = document.getElementById("K" + kguid);
	kdiv.innerHTML = AjaxWortraumControlFlip.MakeKnoten("" + kguid + "").value;
	

}

function ZweigUpdate(item)
{
	var str = new String(item.parentElement.id);
	kguid = str.substring(4,40);
	zguid = str.substring(40);
	void AjaxWortraumControlFlip.UpdateZweig(kguid, zguid, item.value);
	
	var div = item.parentElement;
	div.style.display = "none";	
	
	var zdiv = document.getElementById("Z" + kguid + zguid);
	zdiv.innerHTML = AjaxWortraumControlFlip.MakeZweig("" + kguid + "","" + zguid + "").value;
}
</script>
