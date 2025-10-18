<%@ Page ValidateRequest="false" Language="c#" CodeBehind="TopLabAufTopLab.aspx.cs"
    AutoEventWireup="True" Inherits="OliWeb.Sites.Edit.TopLabAufTopLab" Description="Kommentar auf Antworten"
    MasterPageFile="~/Site.Master" %>

<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopLabKoerper" Src="~/Controls/Koerper/TopLabKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItKoerper" Src="~/Controls/Koerper/PostItKoerper.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>OLI-it: Antwort auf eine Antwort</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <uc1:PostItKoerper ID="PostItKoerper1" runat="server"></uc1:PostItKoerper>
    <uc1:TopLabKoerper ID="TopLabKoerper1" runat="server"></uc1:TopLabKoerper>
    <div class="TopLab">

        <asp:TextBox ID="TitelTextBox" Width="99%" runat="server"></asp:TextBox><br />

        <asp:TextBox ID="TextBox1" runat="server" Width="99%" Rows="10" TextMode="MultiLine"></asp:TextBox><br />
        <asp:Image ID="Image1" runat="server" Width="25px" BorderStyle="None" ImageUrl="~/images/icons/ok.gif"></asp:Image>
        <asp:Button ID="AddButton" runat="server" Text="hinzufügen" OnClick="AddButton_Click"></asp:Button>&nbsp;
                    <asp:Image ID="Image2" runat="server" Width="25px" BorderStyle="None" ImageUrl="~/images/icons/cancel.gif"></asp:Image>
        <asp:Button ID="CancelButton" runat="server" Text="abbrechen" OnClick="CancelButton_Click"></asp:Button>


    </div>
    <br />
    <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe">
        <span style="text-align: right"><a title="online dokumentation" href="http://doku.oli-it.com/Oberflaeche/Oberflaeche.htm"
            target="doku">
            <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                alt="weitere Hilfe" src="../../images/icons/fragezeichen.gif" align="right">
        </a></span>
        <h4>Kommentar</h4>
        <p>
            Kommentieren oder ergänzen sie die Antwort. Eine Antwort auf eine Antwort kann genau
            so bewertet und belohnt werden wie die ursprüngliche Antwort.
        </p>
    </asp:Panel>
</asp:Content>
