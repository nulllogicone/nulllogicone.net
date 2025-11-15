<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../../Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Legende" Src="../../../Sites/Elemente/Legende.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="StammPostItGrid.ascx.cs"
    Inherits="OliWeb.Controls.Koerper.ViewGrids.StammPostItGrid" EnableViewState="True" %>
<div class="rechtsuntenrund">
    <uc1:CloseHyperLink ID="CloseHyperLink1" runat="server" NavigateUrl="~/Sites/StammSite.aspx"
        ToolTip="meine Nachrichten schließen"></uc1:CloseHyperLink>
    <h3>
        <asp:Label ID="TitleLabel" runat="server"></asp:Label></h3>
    <asp:DataGrid ID="PostItDataGrid" runat="server" PageSize="5" AllowPaging="True"
        DataKeyField="PostItGuid" CellSpacing="3" GridLines="None" AutoGenerateColumns="False"
        AllowSorting="True" Width="100%">
        <HeaderStyle Font-Bold="True" CssClass="TableHead"></HeaderStyle>
        <ItemStyle VerticalAlign="Top" />
        <Columns>
            <asp:TemplateColumn SortExpression="Datum" HeaderText="erstellt">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" CssClass="datum" Text='<%# OliEngine.OliUtil.MakeDateTimeDiff(Eval("Datum").ToString()) %>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn SortExpression="anzS" HeaderText="&lt;img border='0' alt='Urheber' width='24' src='../images/icons/Symbole/Stamm.png'&gt;&lt;br /&gt;zahlt">
                <ItemTemplate>
                    <a runat="server" style="text-align: center; display: block;" href='<%# "~/Sites/PostItStammSite.aspx?pguid=" + Eval("PostItGuid") %>'>
                        <asp:Label runat="server" ID="LabelAnzS" Text='<%# Eval("anzS") %>' Font-Bold="True">            
                        </asp:Label><br/>
                             <asp:Label ID="Label2" CssClass="flowKooK" runat="server" Text='<%# OliEngine.OliUtil.MakeRedKook(Eval("bezahlt").ToString() ) %>'
                        ToolTip="Diesen Betrag zahlt der ausgewählte Stamm für diese Nachricht">
                    </asp:Label>
                    </a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn SortExpression="PostIt" HeaderText="Message">
                <ItemStyle CssClass="PostIt" VerticalAlign="Top"></ItemStyle>
                <ItemTemplate>
                    <div>
                        <div class="meta">
                            <%# OliEngine.OliUtil.MakeLinkHtml(HttpUtility.HtmlDecode(Eval("URL").ToString())) %><br />
                            <asp:Label ID="KooKLabel" CssClass="flowKooK" runat="server" Text='<%# OliEngine.OliUtil.MakeRedKook(Eval("KooK").ToString()) %>'>
                            </asp:Label>
                        </div>
                        <div>
                            <a runat="server" href='<%# "~/Sites/PostItSite.aspx?pguid=" + Eval("PostItGuid") %>'>
                                <span style="float: left; margin-right: 1em;">
                                    <%# OliEngine.OliUtil.MakeImageHtml(Eval("Datei").ToString(),50) %></span> <b>
                                        <%# Eval("Titel") %>
                                    </b>
                                <%# OliEngine.OliUtil.FirstXWords (Eval("PostIt").ToString(),30) %>
                            </a>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn SortExpression="KooK" HeaderText="&lt;img border='0' alt='Empf&#228;nger' width='24' src='../images/icons/Symbole/Angler.png' &gt; &lt;br /&gt;Empf.">
                <ItemTemplate>
                    <a runat="server" class="Angler" style="text-align: center; display: block;" href='<%# "~/Sites/PostItAnglerSite.aspx?pguid=" + 
						Eval("PostItGuid") %>'>
                        <%# Eval("anzA") %>
                    </a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn SortExpression="AnzT" HeaderText="&lt;img border='0' alt='Antworten' width='24' src='../images/icons/Symbole/TopLab.png'&gt;&lt;br /&gt;Antw.">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkPostIt" runat="server" CssClass="TopLab" Text='<%# Eval("anzT") %>'
                        NavigateUrl='<%# Eval("PostItGuid","~/Sites/PostItTopLabSite.aspx?pguid={0}") %>'
                        Visible='<%# Eval("anzT").ToString() != "" %>'>ttt</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn SortExpression="Frist" HeaderText="Frist">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# OliEngine.OliUtil.MakeDateTimeDiff(Eval("Frist").ToString()) %>'
                        CssClass="datum">
                    </asp:Label><br />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateColumn>
        </Columns>
        <PagerStyle PageButtonCount="20" CssClass="pager" Mode="NumericPages"></PagerStyle>
    </asp:DataGrid><br />
    <uc1:Legende ID="Legende1" runat="server" Visible="true"></uc1:Legende>
    <%-- TODO: Sortierungs Control um die Spalten des ViewGrids aufzulösen, Legende erklärt--%>
    <%--    <div  style="border: dotted 2px red;">
        <b>Sort:</b> created, Erstellt, Urheber, Alphabetisch, Link, Wert, Klicks, Empfänger,
        Antworten, Frist, ...<br />
        <i>Sortierung und Filter für diese Liste einstellen</i>
    </div>
    <br />--%>
</div>
