<%@ Register TagPrefix="uc1" TagName="KlickBild" Src="../Gimicks/KlickBild.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="EinAusLoggen.ascx.cs"
    Inherits="OliWeb.Controls.Floor.EinAusLoggen" %>
<script type="text/javascript">
    function Toggle(item) {
        var el = document.getElementById(item);
        if (el.style.display == "none") {
            el.style.display = "block";
        }
        else {
            el.style.display = "none";
        }
    }
</script>
<%--Anonym--%>
<asp:Panel ID="AnonymPanel" runat="server" DefaultButton="ShowStammButton" meta:resourcekey="AnonymPanelResource1">
    <table id="TextboxenTable" cellspacing="1" cellpadding="1" bgcolor="#f1f4fa" border="0">
        <tr>
            <td>
               <asp:literal id="LiteralName" text="Name" runat="server" 
                                meta:resourcekey="LiteralNameResource1" />
            </td>
            <td>
               <asp:literal id="LiteralPassword" text="Kennwort" runat="server" 
                                meta:resourcekey="LiteralPasswordResource1" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="StammTextBox" runat="server" Width="145px" meta:resourcekey="StammTextBoxResource1"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="KennwortTextBox" runat="server" Width="145px" TextMode="Password"
                    meta:resourcekey="KennwortTextBoxResource1"></asp:TextBox>
            </td>
            <td rowspan="2">
                <asp:CheckBox ID="MerkenCheckBox" runat="server" ToolTip="Wenn Sie auf diesem Computer automatisch eingeloggt werden wollen"
                    Text="merken" Font-Size="XX-Small" meta:resourcekey="MerkenCheckBoxResource1"></asp:CheckBox>
                <asp:Button ID="ShowStammButton" runat="server" Font-Size="XX-Small" Width="90px"
                    CssClass="EinloggenButton" Text="einloggen" ToolTip="den Stamm anzeigen und wenn möglich einloggen"
                    OnClick="ShowStammButton_Click" meta:resourcekey="ShowStammButtonResource1"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Panel>
<%--Eingeloggt--%>
<asp:Panel ID="EingeloggterStammPanel" runat="server" CssClass="login_stamm" meta:resourcekey="EingeloggterStammPanelResource1">
    <span style="float: left">
        <uc1:KlickBild ID="StammKlickBild" runat="server"></uc1:KlickBild>
    </span>
    <asp:Button ID="AusloggenButton" runat="server" Text="ausloggen" Font-Size="XX-Small"
        Style="float: right;" OnClick="AusloggenButton_Click" meta:resourcekey="AusloggenButtonResource1"></asp:Button>

    <asp:LinkButton ID="StammZeigenLinkbutton" runat="server"
        Font-Size="XX-Small" CssClass="Stamm"
        ToolTip="Daten neu laden" ForeColor="Silver"
        OnClick="StammZeigenLinkbutton_Click"
        meta:resourcekey="StammZeigenLinkbuttonResource1">

        <asp:Literal
            ID="LiteralLoggedIn" Text="eingeloggter Stamm" runat="server"
            meta:resourcekey="LiteralLoggedInResource1" />

    </asp:LinkButton>

    <br />

    <strong>
        <asp:LinkButton ID="EingeloggterStammLinkButton" runat="server" CssClass="Stamm"
            ToolTip="Daten neu laden"
            OnClick="EingeloggterStammLinkButton_Click"
            meta:resourcekey="EingeloggterStammLinkButtonResource1"></asp:LinkButton>&nbsp;</strong>
</asp:Panel>
