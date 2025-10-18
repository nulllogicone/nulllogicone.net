<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LanguageSelect.ascx.cs"
    Inherits="OliWeb.Controls.Gimicks.LanguageSelect" %>
<span style="white-space: nowrap;">
    <asp:LinkButton ID="LinkButtonEn_US" CommandArgument="en-US" Text="EN" runat="server"
        OnClick="LinkButtonCulture_Click" />
    |
    <asp:LinkButton ID="LinkButtonDe_DE" CommandArgument="de-DE" Text="DE" runat="server"
        OnClick="LinkButtonCulture_Click" />  |
    <asp:LinkButton ID="LinkButton_ES" CommandArgument="es-ES" Text="ES" runat="server"
        OnClick="LinkButtonCulture_Click" /></span> 