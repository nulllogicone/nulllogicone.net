<%@ Page Language="c#" CodeBehind="MailSchreiben.aspx.cs" MasterPageFile="~/Site.Master"
    AutoEventWireup="True" Inherits="OliWeb.Sites.Elemente.MailSchreiben" %>

<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>MailSchreiben</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>

    <h3>Hier können Sie
                    <asp:Label ID="StammTxtLabel" runat="server" CssClass="Stamm"></asp:Label>
        eine persönliche Nachricht schreiben.</h3>
    <p>
        Die Email wird über den Server von OLI-it verschickt, damit nicht jeder alle Adressen
                    sehen kann.
    </p>
    <p>
        Sie sind auch nicht irgendwer sondern mit einer IP-Adresse verbunden, die gespeichert
                    wird
                    <asp:Label ID="IPLabel" runat="server" Font-Size="XX-Small"></asp:Label>
    </p>
    <p>
        Sie können gerne nach der persönlichen Adresse fragen und auch&nbsp;kommerzielle
                    Interessen verfolgen wenn sie glauben, den Empfänger nicht zu belästigen.
    </p>


    <table id="MailTable" style="width: 100%;">
        <tr>
            <td style="width: 107px" bgcolor="whitesmoke">an
            </td>
            <td>
                <asp:Label ID="StammLabel" runat="server" CssClass="Stamm"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 107px" bgcolor="whitesmoke">von
            </td>
            <td>
                <asp:TextBox ID="VonTextBox" runat="server" CssClass="TextBoxLine" Width="100%"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td style="width: 107px" bgcolor="whitesmoke">Betreff</td>
            <td>
                <asp:TextBox ID="BetreffTextBox" runat="server" CssClass="TextBoxLine" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td bgcolor="whitesmoke" colspan="2">
                <asp:TextBox ID="BodyTextBox" runat="server" CssClass="textbox" TextMode="MultiLine" Rows="5"
                    Width="100%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div>
        <br/>
    <asp:Button ID="SendenButton" runat="server" Text="senden" OnClick="SendenButton_Click"></asp:Button>
        </div>
</asp:Content>
