<%@ Page language="c#" Codebehind="FeedForm.aspx.cs" AutoEventWireup="false" Inherits="OliWeb.Feed.FeedForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FeedForm</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../OliWeb.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="FeedForm" method="post" runat="server">
			<H2>[OLI-it Feed]</H2>
			<P>Hier werden unterschiedliche, individuell angepasste Daten-Ströme angeboten.</P>
			<P>Neben den bekannten Formen wie&nbsp;News-Feed, Werbebanner, oder FAQ kann man 
				sich eigene Anpassungen ausdenken.</P>
			<P>Die Daten können aus den folgenden Tabellen kommen</P>
			<UL>
				<LI>
				den Nachrichten eines Stamm (P(S))
				<LI>
				den Fischen eines Angler (P(A))
				<LI>
					aus den Antworten auf eine Nachricht (T(P))</LI></UL>
			<H4>Sie haben gemeinsam:</H4>
			<UL>
				<LI>
				Datum
				<LI>
				Bild
				<LI>
				Überschrift
				<LI>
				Text
				<LI>
					Link</LI></UL>
			<H4>Außerdem kann man einstellen</H4>
			<UL>
				<LI>
				wie viele Elemente angezeigt werden sollen
				<LI>
				welches StyleSheet verwendet werden soll
				<LI>
				relative Anzeigehäufigkeit bzgl. KooK,&nbsp;Lohn &amp; Tollis
				<LI>
					Sortierreihenfolge</LI></UL>
			<H4>Beispiele</H4>
			<UL>
				<LI>
				Werbebanner (meine Anzeigen)
				<LI>
				FAQ (fremde Antworten)
				<LI>
				Gästebuch (fremde Einträge)
				<LI>
				cool (subjektive Auswahl)
				<LI>
				Links (kollektiv geführt)
				<LI>
					News (global gefiltert)</LI></UL>
			<P>xx</P>
			<UL>
				<LI>
					<A href="http://localhost/OliWeb/Feed/PostIt/PostItFeedForm.aspx">PostItFeedForm</A>
				<LI>
				</LI>
			</UL>
			<P><BR>
				&nbsp;</P>
			<P>&nbsp;</P>
			<P>&nbsp;</P>
			<P>&nbsp;</P>
		</form>
	</body>
</HTML>
