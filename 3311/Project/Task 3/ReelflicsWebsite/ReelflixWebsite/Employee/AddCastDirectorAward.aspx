<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddCastDirectorAward.aspx.cs" Inherits="ReelflicsWebsite.Employee.AddCastDirectorAward" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <!-- Error message display -->
        <div style="margin-top: 20px">
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="label-info" Visible="False" Height="40px" BackColor="Transparent"></asp:Label>
        </div>
        <h4><span style="text-decoration: underline">Add Academy Award</span></h4>
        <br />
        <!-- Name search -->
        <asp:Panel ID="pnlNameSearch" runat="server">
            <div class="form-group">
                <div class="col-xs-3">
                    <asp:TextBox ID="txtNameSearch" runat="server" CssClass="form-control input-sm" placeholder="Search name" Height="32px" Width="100%"></asp:TextBox>
                </div>
                <div class="col-xs-1">
                    <asp:Button ID="btnNameSearch" runat="server" Text="Search" CssClass="btn btn-sm" OnClick="BtnNameSearch_Click" />
                </div>
                <div class="col-xs-8" style="margin-top: 6px">
                    <asp:Label ID="lblNameSearchResultMessage" runat="server" CssClass="label-info" Visible="False" BackColor="Transparent"></asp:Label>
                </div>
            </div>
        </asp:Panel>
        <!-- Search result -->
        <asp:Panel ID="pnlNameSearchResult" runat="server" Visible="false">
            <h6>Select Name</h6>
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvNameSearchResult" runat="server" CssClass="table-condensed" RowStyle-VerticalAlign="Top"
                        ShowHeader="False" OnRowDataBound="GvNameSearchResult_RowDataBound" BackColor="Transparent" BorderColor="Transparent">
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
        <!-- Movie Person information -->
        <asp:Panel ID="pnlMoviePersonInformation" runat="server" Visible="false">
            <div class="form-group">
                <div class="col-xs-2">
                    <asp:Image runat="server" ID="imgPhoto" ImageUrl="~/images/people/" CssClass="img-responsive center-block" Width="100%" BorderColor="White" BorderWidth="2px"></asp:Image>
                </div>
                <div class="col-xs-10">
                    <!-- Filmography and awards -->
                    <asp:Panel ID="pnlFilmography" runat="server" Visible="false" BackColor="Transparent">
                        <!-- Name -->
                        <h5 style="margin-top: 0px"><asp:Literal ID="litName" runat="server"></asp:Literal></h5>
                        <asp:Label ID="lblNoFilmography" runat="server" Visible="false"></asp:Label>
                        <!-- Actor/actress filmography -->
                        <asp:Panel ID="pnlActorActressFilmography" runat="server" Visible="false">
                            <h6 style="color: navajowhite; margin-top: 12px">
                                <asp:Literal ID="litActorActressHeading" runat="server" Text="Actor"></asp:Literal></h6>
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <asp:GridView ID="gvActorActressFilmography" runat="server" CssClass="table-condensed" Visible="False" RowStyle-VerticalAlign="Top"
                                        ShowHeader="False" OnRowDataBound="GvActorActressFilmography_RowDataBound" BorderColor="Transparent" BackColor="Transparent">
                                    </asp:GridView>
                                </div>
                            </div>
                        </asp:Panel>
                        <!-- Director filmography -->
                        <asp:Panel ID="pnlDirectorFilmography" runat="server" Visible="false">
                            <h6 style="color: navajowhite; margin-top: 12px">Director</h6>
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <asp:GridView ID="gvDirectorFilmography" runat="server" CssClass="table-condensed" Visible="False" RowStyle-VerticalAlign="Top"
                                        ShowHeader="False" OnRowDataBound="GvDirectorFilmography_RowDataBound" BackColor="Transparent" BorderColor="Transparent">
                                    </asp:GridView>
                                </div>
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                </div>
            </div>
        </asp:Panel>
        <!-- Add award panel -->
        <asp:Panel ID="pnlAddAward" runat="server" Visible="false">
            <h5 style="margin-top: 20px">Select Movie And Award</h5>
            <div class="col-xs-12">
                <asp:Label ID="lblAddAwardMessage" runat="server" Visible="false"></asp:Label>
            </div>
            <asp:Panel ID="pnlSelectAward" runat="server" Visible="false">
                <!-- Select movie -->
                <div class="form-group">
                    <asp:Label ID="lblSelectMovie" runat="server" Text="Movie" CssClass="control-label col-xs-1" AssociatedControlID="ddlMovie"></asp:Label>
                    <div class="col-xs-6" style="margin-top: 8px">
                        <asp:DropDownList ID="ddlMovie" runat="server" CssClass="dropdown input-sm" AutoPostBack="True" ToolTip="Select Movie" Height="25px"
                            OnSelectedIndexChanged="DdlMovie_SelectedIndexChanged">
                        </asp:DropDownList>
                        <div class="row">
                            <asp:RequiredFieldValidator ID="rfvDDLMovie" runat="server" ErrorMessage="&nbsp;&nbsp;&nbsp;&nbsp;Please select a movie." ControlToValidate="ddlMovie" CssClass="text-danger"
                                Display="Dynamic" Visible="false" InitialValue="none selected" ValidationGroup="DDLMovie"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <!-- Select award -->
                <div class="form-group">
                    <asp:Label ID="lblSelectAward" runat="server" Text="Award" CssClass="control-label col-xs-1" AssociatedControlID="ddlAward"></asp:Label>
                    <div class="col-xs-8" style="margin-top: 8px">
                        <asp:Label ID="lblSelectAwardMessage" runat="server" Text=" &uarr; First select a movie. &uarr; " CssClass="label-info" BackColor="Transparent" BorderWidth="2px" Height="32px"></asp:Label>
                        <asp:DropDownList ID="ddlAward" runat="server" CssClass="dropdown input-sm" AutoPostBack="True" ToolTip="Select Award" Visible="false" Height="25px"
                            OnSelectedIndexChanged="DdlAward_SelectedIndexChanged">
                        </asp:DropDownList>
                        <div class="row">
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="&nbsp;&nbsp;&nbsp;&nbsp;Please select an award." ControlToValidate="ddlAward" CssClass="text-danger"
                                Display="Dynamic" InitialValue="none selected" ValidationGroup="DDLAward"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-offset-1 col-xs-1">
                        <asp:Button ID="BtnAddCastDirectorAward" runat="server" Text="Add" CssClass="btn btn-sm" OnClick="BtnAddCastDirectorAward_Click" />
                    </div>
                </div>
            </asp:Panel>
        </asp:Panel>
    </div>
</asp:Content>
