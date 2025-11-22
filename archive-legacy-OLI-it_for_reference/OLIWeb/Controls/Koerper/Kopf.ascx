<%@ Register TagPrefix="uc1" TagName="UserToString" Src="../Gimicks/UserToString.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CommandBar" Src="../Command/CommandBar.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="Kopf.ascx.cs" Inherits="OliWeb.Controls.Koerper.Kopf" %>
<%@ Register TagPrefix="uc1" TagName="EinAusLoggen" Src="../Floor/EinAusLoggen.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LogoControl" Src="../Floor/LogoControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="KlickBild" Src="../Gimicks/KlickBild.ascx" %>
<%@ Register Src="../Gimicks/LanguageSelect.ascx" TagName="LanguageSelect" TagPrefix="uc2" %>
<div>
    <div id="EinAusloggenControl" style="float: right;">
        <uc1:EinAusLoggen ID="EinAusLoggen1" runat="server"></uc1:EinAusLoggen>
    </div>
    <div id="LogoControl" style="float:left;">
        <uc1:LogoControl ID="LogoControl1" runat="server"></uc1:LogoControl>
    </div>
    <uc2:LanguageSelect ID="LanguageSelect1" runat="server" />
    <asp:Label ID="NachrichtLabel" runat="server" Font-Size="Smaller" ForeColor="#004000"
        BackColor="LightYellow"></asp:Label>
    <br style="clear: both;" />
</div>
<%--Todo das folgende Control mal einschalten!, bitte nur im Test-Modus ausprobieren, => auf visible="True" umstellen --%>
<uc1:CommandBar ID="CommandBar1" runat="server" Visible="False"></uc1:CommandBar>
<div style="clear:both;"></div>