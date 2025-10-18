<%@ Page Language="c#" CodeBehind="KennwortVergessen.aspx.cs" MasterPageFile="~/Site.Master"
    AutoEventWireup="True" Inherits="OliWeb.Sites.Elemente.KennwortVergessen" %>

<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>KennwortVergessen</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <table id="KennwortVergessenRumpfTable" cellspacing="1" cellpadding="1" width="100%"
        border="0">
        <tr>
            <td>
                <h3>
                    &nbsp;</h3>
                <h3>
                    Hier können Sie&nbsp;
                    <asp:Label ID="StammLabel" runat="server" CssClass="Stamm"></asp:Label>&nbsp;das
                    Kennwort senden.</h3>
                <p>
                    &nbsp;</p>
            </td>
        </tr>
        <tr>
            <td>
                <p>
                    <asp:Button ID="SendenButton" runat="server" Text="senden" OnClick="SendenButton_Click">
                    </asp:Button>&nbsp;(ihre&nbsp;IP wird mitgeschickt&nbsp;
                    <asp:Label ID="IPLabel" runat="server" Font-Size="XX-Small"></asp:Label>)</p>
            </td>
        </tr>
    </table>
</asp:Content>
