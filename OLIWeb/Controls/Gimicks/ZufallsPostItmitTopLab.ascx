<%@ Register TagPrefix="uc1" TagName="KlickBild" Src="KlickBild.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="ZufallsPostItmitTopLab.ascx.cs"
    Inherits="OliWeb.Controls.Gimicks.ZufallsPostItmitTopLab" %>
<table id="ZufallsControlTable" style="width:100%;">
    <tr style="vertical-align:top;">
        <td style="width: 50%">
            <div  class="PostIt" style="overflow: auto;">
                <span style="float: left">
                    <uc1:KlickBild ID="KlickBild1" runat="server"></uc1:KlickBild>
                </span>
                <asp:Label ID="TitelLabel" runat="server" CssClass="titel"></asp:Label><br />
                <asp:HyperLink ID="PostItHyperLink" runat="server"></asp:HyperLink></div>
        </td>
        <td style="width: 50%">
            <div style="overflow: auto;">
                <asp:Repeater ID="TopLabRepeater" runat="server">
                    <ItemTemplate>
                    <div class="TopLab">
                        <span style="float: left">
                            <uc1:KlickBild ID="Klickbild2" runat="server" Breite="100" BildName='<%# Eval("Datei") %>'>
                            </uc1:KlickBild>
                        </span><span class="titel">
                            <%# Eval("Titel") %>
                        </span>
                        <asp:HyperLink ID="Hyperlink1" runat="server" Text='<%# Eval("TopLab") %>'
                            NavigateUrl='<%# "~/T/" + Eval("TopLabGuid") + ".aspx" %>'>
                        </asp:HyperLink>
                       </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </td>
    </tr>
</table>
