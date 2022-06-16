<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lab5.aspx.cs" Inherits="labar5.lab5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Užsakymai</title>
    <link rel="stylesheet" href="style.css" runat="server" type="text/css"/>
  
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div>
            <div>
            <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="True" />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Nuskaityti" Enabled="False" />
            </div>
            <div>
            <asp:FileUpload ID="FileUpload2" runat="server" />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" style="margin-bottom: 0px" Text="Nuskaityti" Enabled="False" />
            </div>
            <asp:Label ID="excError" runat="server" ForeColor="Red"></asp:Label>
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
        </div>
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Skaičiuoti" Enabled="False" />
        <asp:PlaceHolder ID="PlaceHolder3" runat="server"></asp:PlaceHolder>
        <div id="remove" class="none" runat="server">
            <asp:Label ID="Label1" runat="server" Text="Įrašykite maksimalią sumą, kurią galite sumokėti, tam kad ištrintume vertybes."></asp:Label>
            <input id="Text1" type="text" runat="server"/><asp:Button ID="Button4" runat="server" OnClick="Button4_Click1" Text="Trinti" />

            <asp:Label ID="Error" runat="server" ForeColor="Red"></asp:Label>

        </div>
    </form>


     <script type="text/javascript">
         const btn1 = document.getElementById("Button1");
         const btn2 = document.getElementById("Button2");
         const btn3 = document.getElementById("Button3");

         const upl1 = document.getElementById("FileUpload1");
         const upl2 = document.getElementById("FileUpload2");
         upl1.addEventListener("change", () => {
             if(upl1.files.length > 0)
             btn1.disabled = false;
         })
         upl2.addEventListener("change", () => {
             if (document.getElementById("container") && upl2.files.length > 0) {
                 btn2.disabled = false;
             }
         })
         window.addEventListener("load", () => {
             if (document.getElementById("container2")) {
                 btn3.disabled = false;
             }
         })


     </script>
</body>
</html>
