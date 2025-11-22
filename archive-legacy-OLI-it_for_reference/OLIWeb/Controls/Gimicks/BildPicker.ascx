<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="BildPicker.ascx.cs"
    Inherits="OliWeb.Controls.Gimicks.BildPicker" %>
<%@ Import Namespace="Azure.Storage.Blobs.Models" %>
<div>
    <asp:Panel ID="UploadPanel" BackColor="Silver" runat="server" HorizontalAlign="Right">
        <div style="text-align: left">
            neues Bild</div>
        <br />
        <input id="FileSelect" type="file" size="20" name="FileSelect" runat="server" />
        <br />
        <asp:Image ID="OkImage" runat="server" BorderStyle="None" ImageUrl="../../images/icons/ok.gif"
                   Width="25px"></asp:Image><input class="Button" id="UploadButton" type="button" value="Uploaden"
                                                   name="Button1" runat="server" onserverclick="UploadButton_ServerClick" />
    </asp:Panel>
</div>
<div>
<asp:DataList ID="BildDataList" BackColor="#E0E0E0" CellSpacing="10" RepeatDirection="Horizontal"
    RepeatColumns="4" runat="server">
    <FooterTemplate>
        <p>
            <asp:LinkButton ID="Linkbutton1" runat="server" designtimedragdrop="4" Visible="False"
                CommandName="EditLinkButton">neues Bild</asp:LinkButton></p>
    </FooterTemplate>
    <ItemTemplate>
        <asp:ImageButton CssClass="BildPickerBild" ID="ImageButton2" runat="server" ImageUrl="<%# BlobContainer.GetBlobClient(((BlobItem)Container.DataItem).Name).Uri.AbsoluteUri %>"
            Width="80"></asp:ImageButton>
        <asp:Label ID="DateiName" Visible="False" runat="server" Text="<%# BlobContainer.GetBlobClient(((BlobItem)Container.DataItem).Name).Uri.AbsoluteUri %>"
            Font-Size="XX-Small">
        </asp:Label>
    </ItemTemplate>
</asp:DataList>
</div>