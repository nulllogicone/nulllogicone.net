<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Kopf" Src="~/Controls/Koerper/Kopf.ascx" %>
<%@ Page language="c#" Codebehind="BildPickerTest.aspx.cs" AutoEventWireup="True" Inherits="OliWeb.Controls.Gimicks.BildPickerTest" %>
<%@ Register TagPrefix="uc1" TagName="BildPicker" Src="BildPicker.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
    <HEAD>
        <title>BildPickerTest</title>
        <meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
        <meta name="CODE_LANGUAGE" Content="C#">
        <meta name="vs_defaultClientScript" content="JavaScript">
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <LINK href="../../OliWeb.css" type="text/css" rel="stylesheet">
    </HEAD>
    <body >
        <form  method="post" runat="server">
            <P>
                <uc1:Kopf id="Kopf1" runat="server"></uc1:Kopf>
                <uc1:StammKoerper id="StammKoerper1" runat="server"></uc1:StammKoerper>
                <uc1:BildPicker id="BildPicker1" runat="server"></uc1:BildPicker></P>
        </form>
        <!-- (c) Frederic Luchting -->
    </body>
</HTML>