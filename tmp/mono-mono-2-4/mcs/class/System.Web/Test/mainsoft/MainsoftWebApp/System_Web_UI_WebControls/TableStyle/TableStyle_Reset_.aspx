<%@ Register TagPrefix="cc1" Namespace="GHTWebControls" Assembly="MainsoftWebApp" %>
<%@ Page Language="c#" AutoEventWireup="false" Codebehind="TableStyle_Reset_.aspx.cs" Inherits="GHTTests.System_Web_dll.System_Web_UI_WebControls.TableStyle_Reset_" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TableStyle_Reset_</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script LANGUAGE="JavaScript">
        function ScriptTest()
        {
            var theform;
		    if (window.navigator.appName.toLowerCase().indexOf("netscape") > -1) {
			    theform = document.forms["Form1"];
		    }
		    else {
			    theform = document.Form1;
		    }
        }
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<cc1:GHTSubTest id="GHTSubTest1" style="Z-INDEX: 100; LEFT: 16px; POSITION: absolute; TOP: 15px"
				runat="server" Width="488px" Height="232px">
				<asp:Table id="Table1" runat="server" Height="128px" Width="200px" BackColor="#FFFFC0" BorderColor="Navy"
					BorderStyle="Inset" BorderWidth="9px" CellPadding="8" CellSpacing="4" HorizontalAlign="Center"
					GridLines="Both">
					<asp:TableRow>
						<asp:TableCell></asp:TableCell>
						<asp:TableCell></asp:TableCell>
						<asp:TableCell></asp:TableCell>
					</asp:TableRow>
					<asp:TableRow>
						<asp:TableCell></asp:TableCell>
						<asp:TableCell Text="5555"></asp:TableCell>
						<asp:TableCell></asp:TableCell>
					</asp:TableRow>
					<asp:TableRow>
						<asp:TableCell></asp:TableCell>
						<asp:TableCell Text="5555"></asp:TableCell>
						<asp:TableCell></asp:TableCell>
						<asp:TableCell Text="5555"></asp:TableCell>
						<asp:TableCell></asp:TableCell>
					</asp:TableRow>
				</asp:Table>
			</cc1:GHTSubTest>
		</form>
		<br>
		<br>
	</body>
</HTML>
