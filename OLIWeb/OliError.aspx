<%@ Page Language="c#" CodeBehind="OliError.aspx.cs" AutoEventWireup="True" Inherits="OliWeb.OliError" MasterPageFile="~/Site.Master"
    Description="Fehlerseite von OLI-it wird angezeigt, wenn der Server sonst nicht weiter weiß" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/OLI_globus.jpg"></asp:Image>&nbsp;
        - Ups, sorry!</h1>
    <ul>
        <li>Ich kann die Seite nicht finden, can't find the page</li>
        <li>kenne den Parameter nicht, unknown parameter </li>
        <li>oder darf die Funktion nicht ausführen or no rights to execute</li>
    </ul>
    <h2>try</h2>
    <ul>
        <li>den&nbsp;
            <asp:LinkButton ID="BestLinkButton" runat="server" Font-Bold="True" OnClick="BestLinkButton_Click">am besten passenden Link</asp:LinkButton>&nbsp;ausprobieren<br />
        </li>
        <li>go <a href="default.aspx"><strong>Startseite</strong></a><br />
        </li>
        <li>contact <a href="mailto:info@oli-it.com?subject=OliError: "><strong>benachrichtigen</strong></a> </li>
    </ul>
    <h2>Mögliche Ursachen</h2>
    <ul>
        <li>Ein Link ist&nbsp;beim Kopieren und Einfügen durch einen&nbsp;Zeilenumbruch&nbsp;abgeschnitten
            worden. 
            <ul>
                <li>per Hand im Texteditor zusammensetzen<br />
                </li>
            </ul>
        </li>
        <li>Beim Verwenden der 'zurück' - Taste des Browser ist ein veralteter Zustand erreicht
            worden. 
            <ul>
                <li>neu einloggen<br />
                </li>
            </ul>
        </li>
        <li>Der Datenbankserver ist nicht erreichbar 
            <ul>
                <li>später wieder versuchen</li>
            </ul>
        </li>
    </ul>
    <p>
        &nbsp;
    </p>
</asp:Content>
