// --------------------------
// (c) frederic@luchting.de
// 2012-08-16-18:18
// --------------------------
//  

using System.Reflection;

//
// Allgemeine Informationen über eine Assembly werden über folgende Attribute 
// gesteuert. Ändern Sie diese Attributswerte, um die Informationen zu modifizieren,
// die mit einer Assembly verknüpft sind.
//

[assembly: AssemblyTitle("OliWeb")]
[assembly: AssemblyDescription("Webanwendung Oli-it")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("OLI-it")]
[assembly: AssemblyProduct("OLI-it")]
[assembly: AssemblyCopyright("(c) Frederic Luchting 1994-2006")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

//
// Versionsinformationen für eine Assembly bestehen aus folgenden vier Werten:
//
//      Hauptversion
//      Nebenversion 
//      Buildnummer
//      Revision
//
// Sie können alle Werte oder die standardmäßige Revision und Buildnummer 
// mit '*' angeben:

[assembly: AssemblyVersion("4.5.*")]

//
// Um die Assembly zu signieren, müssen Sie einen Schlüssel angeben. Weitere Informationen 
// über die Assemblysignierung finden Sie in der Microsoft .NET Framework-Dokumentation.
//
// Verwenden Sie folgende Attribute, um festzulegen welcher Schlüssel verwendet wird. 
//
// Hinweis: 
//   (*) Wenn kein Schlüssel angegeben ist, wird die Assembly nicht signiert.
//   (*) KeyName verweist auf einen Schlüssel, der im CSP (Crypto Service
//       Provider) auf Ihrem Computer installiert wurde. KeyFile verweist auf eine Datei, die einen
//       Schlüssel enthält.
//   (*) Wenn die Werte für KeyFile und KeyName angegeben werden, 
//       werden folgende Vorgänge ausgeführt:
//       (1) Wenn KeyName im CSP gefunden wird, wird dieser Schlüssel verwendet.
//       (2) Wenn KeyName nicht vorhanden ist und KeyFile vorhanden ist, 
//           wird der Schlüssel in KeyFile im CSP installiert und verwendet.
//   (*) Um eine KeyFile zu erstellen, können Sie das Programm sn.exe (Strong Name) verwenden.
//        Wenn KeyFile angegeben wird, muss der Pfad von KeyFile
//        relativ zum "Projektausgabeverzeichnis" sein. Der Pfad des Projektausgabeverzeichnisses
//        hängt davon ab, ob Sie mit einem lokalen oder einem Webprojekt arbeiten.
//        Für lokale Projekte wird das Verzeichnis wie folgt definiert:
//       <Project Directory>\obj\<Configuration>. Wenn sich KeyFile z.B.
//       im Projektverzeichnis befindet, geben Sie das AssemblyKeyFile-Attribut 
//       wie folgt an: [assembly: AssemblyKeyFile("..\\..\\mykey.snk")]
//        Für Webprojekte wird das Projektausgabeverzeichnis wie folgt definiert:
//       %HOMEPATH%\VSWebCache\<Machine Name>\<Project Directory>\obj\<Configuration>.
//   (*) Das verzögern der Signierung ist eine erweiterte Option. Weitere Informationen finden Sie in der
//       Microsoft .NET Framework-Dokumentation.
//

[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyFile("")]
[assembly: AssemblyKeyName("")]