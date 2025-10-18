<%@ Page Language="c#" CodeBehind="FreundWerben.aspx.cs" AutoEventWireup="True" Inherits="OliWeb.Sites.Elemente.FreundWerben" %>

<%@ Register TagPrefix="uc1" TagName="Kopf" Src="~/Controls/Koerper/Kopf.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>FreundWerben</title>
</head>
<body>
    <form method="post" runat="server">
        <%--TODO: include this site to the MasterPage! --%>
    <p>
        <uc1:Kopf ID="Kopf1" runat="server"></uc1:Kopf>
    </p>
    <table id="FreundWerbenTable" cellspacing="1" cellpadding="1" width="100%" border="0">
        <tr>
            <td width="50">
            </td>
            <td>
                <p>
                    <asp:Label ID="Label1" runat="server">Label</asp:Label></p>
                <p>
                    Werben Sie Freunde! Sie&nbsp;bekommen für jede Neuanmeldung
                    <asp:Label ID="KooKLabel" runat="server" CssClass="boundKooK">10</asp:Label>&nbsp;<strong>KooK
                    </strong>gutgeschrieben.</p>
                <p>
                    Sie können beliebig viele Empfängeradressen mit Semikolon getrennt eingeben und
                    den Text beliebig ändern oder ergänzen.
                </p>
                <p>
                    Wenn der Empfänger den Link in der Nachricht für eine Neuanmeldung verwendet, werden
                    Sie automatisch begünstigt.&nbsp;&nbsp;Ansonsten kann&nbsp;er bei&nbsp;der Neuanmeldung
                    Ihre Emailadresse angeben.</p>
                <p>
                    <table id="FreundTable" cellspacing="1" cellpadding="1" width="300" border="1">
                        <tr>
                            <td bgcolor="#d3d3d3">
                                Von:
                            </td>
                            <td>
                                <asp:TextBox ID="VonTextBox" runat="server" Width="520"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                    ControlToValidate="VonTextBox" ErrorMessage="<br />Bitte geben Sie ihre Emailadresse an"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="lightgrey">
                                An:
                            </td>
                            <td>
                                <asp:TextBox ID="AnTextBox" runat="server" Width="520"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                    ControlToValidate="AnTextBox" ErrorMessage="<br />Bitte geben Sie mindestens eine Empfängeradressen an"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="lightgrey">
                                Betreff:
                            </td>
                            <td>
                                <asp:TextBox ID="BetreffTextBox" runat="server" Width="520"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:TextBox ID="BodyTextBox" runat="server" Width="600" TextMode="MultiLine" Height="267px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </p>
                <p>
                    <asp:Button ID="SendenButton" runat="server" CssClass="Button" designtimedragdrop="19"
                        Text="senden" OnClick="SendenButton_Click"></asp:Button>&nbsp;
                    <asp:Button ID="CancelButton" runat="server" CssClass="Button" Text="abbrechen" CausesValidation="False"
                        OnClick="CancelButton_Click"></asp:Button></p>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
