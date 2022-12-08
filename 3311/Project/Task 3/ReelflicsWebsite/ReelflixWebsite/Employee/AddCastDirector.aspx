<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddCastDirector.aspx.cs" Inherits="ReelflicsWebsite.Employee.AddCastDirector" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type='text/javascript'>
        function UploadFile(fileUpload) {
            if (fileUpload.value != "") {
                document.getElementById("<%=btnUploadPhoto.ClientID %>").click();
            }
        }
    </script>
    <div class="form-horizontal">
        <!-- Error message display -->
        <div style="margin-top: 20px">
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="label-info" Visible="False" Height="40px" BackColor="Transparent"></asp:Label>
        </div>
        <h4 style="text-decoration: underline">Add Cast Member/Director</h4>
        <br />
        <!-- Movie person information panel -->
        <asp:Panel ID="pnlAddMoviePerson" runat="server" Visible="true">
            <div class="form-group">
                <!-- Photo -->
                <div class="col-xs-2">
                    <asp:Image runat="server" ID="imgPhoto" ImageUrl="~/images/people/0-photo-placeholder.jpg" CssClass="img-responsive center-block" Width="100%"
                        BorderColor="White" BorderWidth="2px"></asp:Image>
                    <div style="text-align: center; margin-top: 10px">
                        <asp:Label ID="lblUploadMessage" runat="server" CssClass="label-info" Text="Choose a photo." BackColor="Transparent"></asp:Label>
                        <asp:TextBox ID="txtUploadMessage" runat="server" Style="display: none"></asp:TextBox>
                        <div>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUploadMessage" ErrorMessage="A photo is required." CssClass="text-danger" Display="Dynamic"
                                EnableClientScript="False"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div style="margin-left: 20px">
                        <asp:FileUpload ID="fuPhoto" runat="server" Width="90px" ToolTip="Choose a photo" OnChange="UploadFile(this)" />
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="fuPhoto" ValidationExpression=".+\.(jpg|JPG)$" ErrorMessage="Only jpg file type is allowed."
                            CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ValidationGroup="PhotoUpload"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-xs-10">
                 <!-- Name -->
                   <div class="row" style="margin-top: 0px">
                        <asp:Label runat="server" Text="Name" AssociatedControlID="txtName" CssClass="col-xs-1 control-label"></asp:Label>
                        <div class="col-xs-11">
                            <asp:TextBox runat="server" ID="txtName" CssClass="form-control input-sm" MaxLength="35" ToolTip="Name" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName" ErrorMessage="A name is required." CssClass="text-danger"
                                Display="Dynamic" EnableClientScript="False" />
                        </div>
                    </div>
                    <!-- Biography -->
                    <div class="row" style="margin-top: 10px">
                        <asp:Label runat="server" Text="Biography" AssociatedControlID="txtBiography" CssClass="col-xs-1 control-label"></asp:Label>
                        <div class="col-xs-11">
                            <asp:TextBox runat="server" ID="txtBiography" CssClass="form-control input-sm" MaxLength="500" ToolTip="Biography"
                                TextMode="MultiLine" Height="80px"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBiography" ErrorMessage="A biography is required." CssClass="text-danger"
                                Display="Dynamic" EnableClientScript="False" />
                        </div>
                    </div>
                    <!-- Gender -->
                    <div class="row" style="margin-top: 10px">
                        <asp:Label runat="server" Text="Gender" AssociatedControlID="ddlGender" CssClass="col-xs-1 control-label"></asp:Label>
                        <div class="col-xs-1">
                            <asp:DropDownList ID="ddlGender" runat="server" CssClass="dropdown input-sm" Height="25px">
                                <asp:ListItem Value="M">Male</asp:ListItem>
                                <asp:ListItem Value="F">Female</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <!-- Birthdate -->
                    <div class="row" style="margin-top: 10px">
                        <asp:Label runat="server" Text="Birthdate" AssociatedControlID="txtBirthdate" CssClass="col-xs-1 control-label"></asp:Label>
                        <div class="col-xs-2">
                            <asp:TextBox runat="server" ID="txtBirthdate" CssClass="form-control input-sm" MaxLength="11" Placeholder="dd-mmm-yyyy"
                                ToolTip="Birthdate" Width="100px"></asp:TextBox>
                            <asp:CustomValidator ID="cvBirthdate" runat="server" ControlToValidate="txtBirthdate" ErrorMessage="Please enter a valid date." CssClass="text-danger"
                                Display="Dynamic" EnableClientScript="False"
                                OnServerValidate="CvBirthdate_ServerValidate"></asp:CustomValidator>
                        </div>
                    </div>
                    <!-- Deathdate -->
                    <div class="row" style="margin-top: 10px">
                        <asp:Label runat="server" Text="Deathdate" AssociatedControlID="txtDeathdate" CssClass="col-xs-1 control-label"></asp:Label>
                        <div class="col-xs-2">
                            <asp:TextBox runat="server" ID="txtDeathdate" CssClass="form-control input-sm" MaxLength="11" Placeholder="dd-mmm-yyyy"
                                ToolTip="Deathdate" Width="100px"></asp:TextBox>
                            <asp:CustomValidator ID="cvDeathdate" runat="server" ControlToValidate="txtDeathdate" ErrorMessage="Please enter a valid date." CssClass="text-danger"
                                Display="Dynamic" EnableClientScript="False"
                                OnServerValidate="CvDeathdate_ServerValidate"></asp:CustomValidator>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-xs-offset-3 col-xs-1">
                    <asp:Button ID="BtnAddCastDirector" runat="server" Text="Add" CssClass="btn btn-sm" OnClick="BtnAddCastDirector_Click" />
                </div>
                <div class="col-xs-2">
                    <asp:Button ID="btnUploadPhoto" runat="server" OnClick="BtnUploadPhoto_Click" CausesValidation="True" ValidationGroup="PhotoUpload" Style="display: none" />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
