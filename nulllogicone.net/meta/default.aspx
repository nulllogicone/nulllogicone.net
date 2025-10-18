<%@ Page Language="c#" CodeBehind="default.aspx.cs" AutoEventWireup="True" Inherits="nulllogicone.net.meta._default" %>

<%@ Register TagPrefix="uc1" TagName="FussZeile" Src="../FussZeile.ascx" %>
<!DOCTYPE HTML>
<html>
<head>
    <title>meta default</title>
    <link href="../nulllogicone.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <h1>Meta</h1>
        <p>
            Das<a href="http://www.metaweb-project.net"> MetaWeb-Project</a> ermöglicht es, 
				alle adressierbaren Inhalte mit einer Markierung zu versehen.
        </p>
        <h2>Einbinden bei Webseiten</h2>
        <p>
            Es gibt mehrere Möglichkeiten eine Markierung für eine URL zur Verfügung zu 
				stellen.
        </p>
        <ol>
            <li>In dem Seitenheader&nbsp;als &lt;!--&lt;xml&gt;--&gt; Konstrukt. Damit steht es 
					zwar direkt neben dem Inhalt, kann vom Benutzer direkt betrachtet und von 
					Suchmaschinen berücksichtigt werden - dafür bläht es auch den Quellcode nicht 
					unerheblich auf.<br>
                <br>
                <li>Verlink zu einer extra Seite (kann auch über Parameter gesteuert eine 
					dynamische sein), die das &lt;xml&gt;Markierungskonstrukt zurückgibt.<br>
                    <br>
                    <font face="Courier New">&lt;link rel= "<strong>CodeDataSet</strong>" type= 
						"text/xml" href= "nebenderseite.xml"&nbsp;/&gt;</font>
                    <br>
                    <br>
                    <li>Angabe einer pguid, wenn man die Markierung bei einem online Dienst erstellt 
					hat und sie über diesen Server erreichbar ist. Dann können mehrere Markierungen 
					verfügbar sein.<br>
                        <br>
                        <font face="Courier New">&lt;rdf:RDF xmlns:rdf=</font><a href="http://www.w3.org/1999/02/22-rdf-syntax-ns#"><font face="Courier New">http://www.w3.org/1999/02/22-rdf-syntax-ns#</font></a><font face="Courier New">
						<br>
						&nbsp;&nbsp; xmlns:nlo=</font><a href="https://nulllogicone.net/schemata/"><font face="Courier New">https://nulllogicone.net/schemata/</font></a><font face="Courier New">&gt;
						<br>
						&nbsp;&nbsp;&lt;rdf:Description&nbsp;
						<br>
						&nbsp;&nbsp;&nbsp;rdf:about=</font><a href="http://domain.tld/die_betreffende_Seite.htm"><font face="Courier New">http://domain.tld/die_betreffende_Seite.htm</font></a><font face="Courier New">&gt;&nbsp;<br>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;oli:host&gt;<a href="http://service.oli-it.com"><a href="http://service.oli-it.com/m">http://service.oli-it.com/</a></a></font><font face="Courier New">&lt;/oli:host&gt;</font><font face="Courier New">
						<br>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;oli:sguid&gt;xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx&lt;/oli:sguid&gt;<br>
						&nbsp;&nbsp;&nbsp;&nbsp; 
						&lt;oli:pguid&gt;xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx&lt;/oli:sguid&gt;&nbsp;&nbsp;&nbsp;&nbsp;</font><font face="Courier New">
						<br>
						&nbsp;&nbsp;&lt;/rdf:Description&gt;
						<br>
						&lt;/rdf:RDF&gt;</font></li>
        </ol>
        <p>&nbsp;</p>
        <p>
            <uc1:FussZeile ID="FussZeile1" runat="server"></uc1:FussZeile>
        </p>
    </form>
</body>
</html>
