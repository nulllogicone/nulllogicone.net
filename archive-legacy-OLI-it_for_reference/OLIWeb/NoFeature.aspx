<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="Controls/Koerper/StammKoerper.ascx" %>
<%@ Page language="c#" Codebehind="NoFeature.aspx.cs" AutoEventWireup="True" Inherits="OliWeb.NoFeature" %>
<%@ Register TagPrefix="uc1" TagName="CommandTree" Src="Controls/Command/CommandTree.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Kopf" Src="Controls/Koerper/Kopf.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
    <HEAD>
        <title>NoCookie</title>
        <LINK href="OliWeb.css" type="text/css" rel="stylesheet" />
        <script language="javascript" type="text/javascript">
            function show() {
                msg = document.getElementById("java");
                var oP = document.createElement("p");
                var oText = document.createTextNode("Java ist aktiviert");
                oP.appendChild(oText);
                msg.appendChild(oP);
            }
        </script>
    </HEAD>
    <body onload="show()"  >
        <FORM  method="post" encType="multipart/form-data" runat="server">
            <uc1:kopf id=Kopf1 runat="server"></uc1:kopf>
            <TABLE id=BodyTable cellspacing=1 cellpadding=1 border=0>
                <TR>
                    <TD vAlign=top><uc1:commandtree id=CommandTree1 runat="server"></uc1:commandtree></TD>
                    <TD vAlign=top><uc1:stammkoerper id=StammKoerper1 runat="server"></uc1:stammkoerper><br />
                        <asp:panel id=HilfePanel runat="server"  enableviewstate="False" cssclass="rechtsobenrund Hilfe">
                            <DIV><SPAN style="TEXT-ALIGN: right"><A title="online dokumentation" href="#" target="doku"><IMG class="fragezeichen" id="Hilfe" style="BORDER-BOTTOM: 0px; BORDER-LEFT: 0px; BORDER-RIGHT: 0px; BORDER-TOP: 0px;"
                                                                                                                             alt="weitere Hilfe" src="images/icons/fragezeichen.gif" align=right> </A>
                                 </SPAN>
                            </DIV>
                            <H1>Feature vermisst</H1>
                            <H2>Cookies</H2>
                            <P>
                                <asp:Label id="NoCookieLabel" runat="server" ForeColor="Red"></asp:Label></P>
                            <P>Um sich bei dieser Webanwendung als Benutzer anmelden zu können, muss Ihr 
                                Browser Session Cookies akzeptieren.&nbsp;Sie speichern keinerlei persönliche 
                                Daten und werden beim Schließen des Browsers&nbsp;automatisch gelöscht. Bitte 
                                stellen Sie in ihrem Browser ein, daß Cookies von dieser Domain (oli-it.com) 
                                akzeptiert werden.</P>
                            <H2>JavaScript</H2>
                            <NOSCRIPT>
                                <DIV style="COLOR: red">JavaScript ist nicht aktiviert</DIV>
                            </NOSCRIPT>
                            <DIV id="java" style="COLOR: green"></DIV>
                            <P>JavaScript ist keine zwingende Vorraussetzung für die Benutzung dieser 
                                Webanwendung aber es erleichtert die Bedienung. Manche Funktionen stehen ohne 
                                JavaScript nicht zur Verfügung.</P>
                            <P>&nbsp;</P>
                        </asp:panel></TD>
                </TR>
            </TABLE>
        </FORM>
    </body>
</HTML>