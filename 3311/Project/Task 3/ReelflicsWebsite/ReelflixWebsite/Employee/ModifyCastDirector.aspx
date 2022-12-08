<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModifyCastDirector.aspx.cs" Inherits="ReelflicsWebsite.Employee.ModifyCastDirector" %>

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
        <h4><span style="text-decoration: underline">Modify Cast Member/Director</span></h4>
        <br />
        <!-- Name search  -->
        <asp:Panel ID="pnlNameSearch" runat="server">
            <div class="form-group">
                <div class="col-xs-3">
                    <asp:TextBox ID="txtSearchName" runat="server" CssClass="form-control input-sm" placeholder="Search name" Height="32px" Width="100%"></asp:TextBox>
                </div>
                <div class="col-xs-1">
                    <asp:Button ID="btnSearchName" runat="server" Text="Search" CssClass="btn btn-sm" OnClick="BtnSearchName_Click" />
                </div>
                <div class="col-xs-8>">
                    <asp:Label ID="lblSearchResultMessage" runat="server" CssClass="label-info" Visible="False" BackColor="Transparent"></asp:Label>
                </div>
            </div>
            <!-- Name search result -->
            <asp:Panel ID="pnlSearchResult" runat="server" Visible="false">
                <h6>Select Cast Member/Director</h6>
                <div class="form-group">
                    <div class="col-xs-12">
                        <asp:GridView ID="gvSearchResult" runat="server" CssClass="table-responsive" CellPadding="5" CellSpacing="3" RowStyle-VerticalAlign="Top"
                            ShowHeader="False" OnRowDataBound="GvSearchResult_RowDataBound" BackColor="Transparent" BorderColor="Transparent">
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
        </asp:Panel>
        <!-- Movie person record -->
        <asp:Panel ID="pnlMoviePersonRecord" runat="server" Visible="false">
            <div class="form-group">
                <!-- Photo -->
                <div class="col-xs-2">
                    <asp:Image runat="server" ID="imgPhoto" CssClass="img-responsive center-block" Width="100%" BorderColor="White" BorderWidth="2px"></asp:Image>
                    <div style="text-align: center; margin-top: 10px">
                        <asp:Label ID="lblUploadMessage" runat="server" CssClass="label-info" Text="Choose a photo." BackColor="Transparent"></asp:Label>
                    </div>
                    <div style="margin-left: 20px">
                        <asp:FileUpload ID="fuPhoto" runat="server" Width="90px" ToolTip="Choose a photo" OnChange="UploadFile(this)" />
                        <div>
                            <asp:RegularExpressionValidator runat="server" ErrorMessage="Only jpg file type is allowed." ValidationExpression=".+\.(jpg|JPG)$" ControlToValidate="fuPhoto"
                                CssClass="text-danger" EnableClientScript="False" Display="Dynamic" ValidationGroup="UploadPhoto"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <div class="col-xs-10">
                    <!-- Name -->
                    <div class="row" style="margin-top: 0px">
                        <asp:Label runat="server" Text="Name" AssociatedControlID="txtName" CssClass="col-xs-1 control-label"></asp:Label>
                        <div class="col-xs-11">
                            <asp:TextBox runat="server" ID="txtName" CssClass="form-control input-sm" MaxLength="35" ToolTip="Name" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName" CssClass="text-danger"
                                ErrorMessage="A name is required." Display="Dynamic" EnableClientScript="False" />
                        </div>
                    </div>
                    <!-- Biography -->
                    <div class="row" style="margin-top: 10px">
                        <asp:Label runat="server" Text="Biography" AssociatedControlID="txtBiography" CssClass="col-xs-1 control-label"></asp:Label>
                        <div class="col-xs-11">
                            <asp:TextBox runat="server" ID="txtBiography" CssClass="form-control input-sm" MaxLength="500" ToolTip="Biography"
                                TextMode="MultiLine" Height="80px"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBiography" CssClass="text-danger"
                                ErrorMessage="A biography is required." Display="Dynamic" EnableClientScript="False" />
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
                        <div class="col-xs-3">
                            <asp:TextBox runat="server" ID="txtBirthdate" CssClass="form-control input-sm" MaxLength="11" Placeholder="dd-mmm-yyyy"
                                ToolTip="Birthdate" Width="100px"></asp:TextBox>
                            <asp:CustomValidator ID="cvBirthdate" runat="server" ControlToValidate="txtBirthdate" CssClass="text-danger"
                                Display="Dynamic" EnableClientScript="False" ErrorMessage="Please enter a valid date."
                                OnServerValidate="CvBirthdate_ServerValidate"></asp:CustomValidator>
                        </div>
                    </div>
                    <!-- Deathdate -->
                    <div class="row" style="margin-top: 10px">
                        <asp:Label runat="server" Text="Deathdate" AssociatedControlID="txtDeathdate" CssClass="col-xs-1 control-label"></asp:Label>
                        <div class="col-xs-3">
                            <asp:TextBox runat="server" ID="txtDeathdate" CssClass="form-control input-sm" MaxLength="11" Placeholder="dd-mmm-yyyy"
                                ToolTip="Deathdate" Width="100px"></asp:TextBox>
                            <asp:CustomValidator ID="cvDeathdate" runat="server" ControlToValidate="txtDeathdate" CssClass="text-danger"
                                Display="Dynamic" EnableClientScript="False" ErrorMessage="Please enter a valid date."
                                OnServerValidate="CvDeathdate_ServerValidate"></asp:CustomValidator>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group" style="margin-top: 10px">
            </div>
            <div class="form-group">
                <div class="col-xs-offset-3 col-xs-1">
                    <asp:Button ID="btnBtnModifyCastDirector" runat="server" Text="Modify" CssClass="btn btn-sm" OnClick="BtnModifyCastDirector_Click" />
                </div>
                <div class="col-xs-6">
                    <asp:Label ID="lblModifyCastDirectorMessage" runat="server" CssClass="label-info" Visible="false" BackColor="Transparent"></asp:Label>
                </div>
                <div class="col-xs-2">
                    <asp:Button ID="btnUploadPhoto" runat="server" OnClick="BtnUploadPhoto_Click" CausesValidation="True" ValidationGroup="UploadPhoto" Style="display: none" />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
