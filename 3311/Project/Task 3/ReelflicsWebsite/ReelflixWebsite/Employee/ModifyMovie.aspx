<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModifyMovie.aspx.cs" Inherits="ReelflicsWebsite.Employee.ModifyMovie" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
    <script src="../Scripts/jquery.sumoselect.min.js"></script>
    <link href="../Content/sumoselect.css" rel="stylesheet">

    <script type="text/javascript">
        $(document).ready(function () {
            $(<%=lbxGenres.ClientID%>).SumoSelect({ placeholder: 'Select genres' });
        });

        function UploadFile(fileUpload) {
            if (fileUpload.value != "") {
                document.getElementById("<%=btnUploadPoster.ClientID %>").click();
            }
        }
    </script>

    <div class="form-horizontal">
        <!-- Error message display -->
        <div style="margin-top: 20px">
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="label-info" Visible="False" Height="40px" BackColor="Transparent"></asp:Label>
        </div>
        <h4><span style="text-decoration: underline">Modify Movie</span></h4>
        <br />
        <!-- Search movie -->
        <asp:Panel ID="pnlMovieSearch" runat="server" Visible="false">
            <div class="form-group">
                <div class="col-xs-3">
                    <asp:TextBox ID="txtMovieSearch" runat="server" CssClass="form-control input-sm" placeholder="Search title" Height="32px" Width="100%"></asp:TextBox>
                </div>
                <div class="col-xs-1">
                    <asp:Button ID="btnMovieSearch" runat="server" Text="Search" CssClass="btn btn-sm" OnClick="BtnMovieSearch_Click" />
                </div>
                <div class="col-xs-8" style="margin-top: 6px">
                    <asp:Label ID="lblMovieSearchResultMessage" runat="server" CssClass="label-info" Visible="False" BackColor="Transparent"></asp:Label>
                </div>
            </div>
            <!-- Search movie result -->
            <asp:Panel ID="pnlMovieSearchResult" runat="server" Visible="false">
                <h6>Select Movie</h6>
                <div class="form-group">
                    <div class="col-xs-12">
                        <asp:GridView ID="gvMovieSearchResult" runat="server" CssClass="table-condensed" CellPadding="5" CellSpacing="3" RowStyle-VerticalAlign="Top"
                            ShowHeader="False" OnRowDataBound="GvMovieSearchResult_RowDataBound" BackColor="Transparent" BorderColor="Transparent" Font-Size="Small">
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
        </asp:Panel>
        <!-- Movie information -->
        <asp:Panel ID="pnlMovieInformation" runat="server" Visible="false">
            <!-- Title and best picture -->
            <div class="form-group">
                <asp:Label ID="lblTitle" runat="server" Text="Title" CssClass="col-xs-1 control-label" AssociatedControlID="txtTitle"></asp:Label>
                <div class="col-xs-7">
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control input-sm" MaxLength="70" ToolTip="Title"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTitle" ErrorMessage="Title is required." CssClass="text-danger" Display="Dynamic"
                        EnableClientScript="False"></asp:RequiredFieldValidator>
                </div>
                <div class="col-xs-2">
                    <asp:Image ID="imgBestPicture" runat="server" ImageUrl="~/images/oscar.png" BackColor="Transparent" BorderColor="Transparent" CssClass="img-thumbnail" Height="30px" />
                    <asp:Label ID="lblBestPicture" runat="server" Text="Best Picture" CssClass="control-label" AssociatedControlID="rblIsBestPicture"></asp:Label>
                </div>
                <div class="col-xs-2">
                    <asp:RadioButtonList ID="rblIsBestPicture" runat="server" CssClass="" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem class="radio-inline" Value="N" Selected="True">No</asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="Y">Yes</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <!-- Poster -->
            <div class="form-group">
                <div class="col-xs-3">
                    <asp:Panel ID="pnlPoster" runat="server" Visible="false">
                        <asp:Image runat="server" ID="imgPoster" ImageUrl="~/images/posters/" CssClass="img-responsive center-block" Width="100%"
                            BorderColor="White" BorderWidth="2px"></asp:Image>
                        <div style="text-align: center; margin-top: 10px">
                            <asp:Label ID="lblUploadMessage" runat="server" CssClass="label-info" Text="Choose a poster." BackColor="Transparent"></asp:Label>
                            <div>
                                <asp:TextBox ID="txtUploadMessage" runat="server" Style="display: none"></asp:TextBox>
                            </div>
                        </div>
                        <div style="margin-left: 60px">
                            <asp:FileUpload ID="fuPoster" runat="server" Width="90px" Visible="false" ToolTip="Choose a poster" OnChange="UploadFile(this)" />
                            <div>
                                <asp:RegularExpressionValidator runat="server" ControlToValidate="fuPoster" ErrorMessage="Only jpg file type is allowed." ValidationExpression=".+\.(jpg|JPG)$"
                                    CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ValidationGroup="UploadPoster"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="col-xs-9">
                    <div class="row">
                        <!-- Release year and MPAA rating -->
                        <asp:Label ID="lblReleaseYear" runat="server" Text="Release Year" CssClass="col-xs-2 control-label" AssociatedControlID="txtReleaseYear"></asp:Label>
                        <div class="col-xs-2">
                            <asp:TextBox ID="txtReleaseYear" runat="server" CssClass="form-control input-sm" Width="50px" MaxLength="4" ToolTip="Release year"></asp:TextBox>
                        </div>
                        <asp:Label ID="lblMPAARating" runat="server" Text="MPAA Rating" CssClass="col-xs-3 control-label" AssociatedControlID="ddlMPAARating"></asp:Label>
                        <div class="col-xs-2" style="margin-top: 2px">
                            <asp:DropDownList ID="ddlMPAARating" runat="server" CssClass="dropdown input-sm" Height="25px" ToolTip="MPAA rating">
                                <asp:ListItem>G</asp:ListItem>
                                <asp:ListItem>PG</asp:ListItem>
                                <asp:ListItem>PG-13</asp:ListItem>
                                <asp:ListItem>R</asp:ListItem>
                                <asp:ListItem>Approved</asp:ListItem>
                                <asp:ListItem>Passed</asp:ListItem>
                                <asp:ListItem>Not Rated</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 0px">
                        <div class="col-xs-offset-2 col-xs-4">
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtReleaseYear" ErrorMessage="Release year is required." CssClass="text-danger"
                                Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator runat="server" ControlToValidate="txtReleaseYear" ValidationExpression="^188[8-9]|189\d|19\d\d|20[0-1]\d|202[0-2]$"
                                ErrorMessage="Year is not valid." CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <!-- Running time and IMDB rating -->
                    <div class="row" style="margin-top: 20px">
                        <asp:Label ID="lblRunningTime" runat="server" Text="Running Time" CssClass="col-xs-2 control-label" AssociatedControlID="txtRunningTime"></asp:Label>
                        <div class="col-xs-2">
                            <asp:TextBox ID="txtRunningTime" runat="server" CssClass="form-control input-sm" Placeholder="min." Width="50px" MaxLength="3" ToolTip="Running time"></asp:TextBox>
                        </div>
                        <asp:Label ID="lblIMDBRating" runat="server" Text="IMDB Rating" CssClass="col-xs-3 control-label" AssociatedControlID="txtIMDBRating"></asp:Label>
                        <div class="col-xs-2">
                            <asp:TextBox ID="txtIMDBRating" runat="server" CssClass="form-control input-sm" Width="50px" MaxLength="4"></asp:TextBox>
                        </div>
                        <div class="row" style="margin-top: 0px">
                            <div class="col-xs-offset-2 col-xs-5">
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtRunningTime" ErrorMessage="Running time is required." CssClass="text-danger"
                                    Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtRunningTime" ErrorMessage="Time is not valid."
                                    ValidationExpression="\d{1,3}" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RegularExpressionValidator>
                                <asp:CompareValidator runat="server" ControlToValidate="txtRunningTime" ErrorMessage="Time must be greater than 0." Operator="GreaterThan"
                                    ValueToCompare="0" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:CompareValidator>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtIMDBRating" ErrorMessage="IMDB rating is required." CssClass="text-danger"
                                    Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
                                <asp:RangeValidator runat="server" ControlToValidate="txtIMDBRating" MaximumValue="10.0" MinimumValue="1.0" Type="Double"
                                    ErrorMessage="Rating must be between 1 and 10." CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RangeValidator>
                            </div>
                        </div>
                    </div>
                    <!-- Movie genres -->
                    <div class="row" style="margin-top: 20px">
                        <asp:Label ID="lblGenres" runat="server" Text="Movie Genres" CssClass="col-xs-2 control-label" AssociatedControlID="lbxGenres"></asp:Label>
                        <div class="col-xs-3">
                            <asp:ListBox runat="server" ID="lbxGenres" SelectionMode="Multiple" CssClass="form-control input" Width="200px" Height="30px" ToolTip="Genres"></asp:ListBox>
                        </div>
                        <div class="col-xs-offset-2 col-xs-5">
                            <asp:TextBox ID="txtGenres" runat="server" CssClass="form-control input-sm" placeholder="Add new genres (comma separated)"
                                ToolTip="Add new genres" Height="32px"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 0px">
                        <div class="col-xs-offset-2 col-xs-10">
                            <asp:CustomValidator ID="cvNewMovieGenres" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"
                                OnServerValidate="CvNewMovieGenres_ServerValidate" ValidationGroup="NewMovieGenres"></asp:CustomValidator>
                            <asp:CustomValidator ID="cvMovieGenres" runat="server" ErrorMessage="Select one or more genres and/or add one or more new genres."
                                CssClass="text-danger" Display="Dynamic" EnableClientScript="False" OnServerValidate="CvMovieGenres_ServerValidate"></asp:CustomValidator>
                        </div>
                    </div>
                    <!-- Synopsis -->
                    <div class="row" style="margin-top: 20px">
                        <asp:Label ID="lblSynopsis" runat="server" Text="Synopsis" CssClass="col-xs-2 control-label" AssociatedControlID="txtSynopsis"></asp:Label>
                        <div class="col-xs-10" style="margin-top: 8px">
                            <asp:TextBox ID="txtSynopsis" runat="server" CssClass="form-control input-sm" ToolTip="Synopsis" TextMode="MultiLine" Height="80px" MaxLength="300"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSynopsis" ErrorMessage="Synopsis is required." CssClass="text-danger"
                                Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <!-- Director and cast information -->
        <asp:Panel ID="pnlCastDirectorsInformation" runat="server" Visible="false">
            <!-- Add director -->
            <asp:Panel ID="pnlDirectors" runat="server" Visible="false">
                <hr />
                <div class="form-group">
                    <div class="col-xs-1">
                        Directors
                    </div>
                    <div class="col-xs-5" style="margin-top: 2px">
                        <asp:GridView ID="gvDirectors" runat="server" CssClass="table-responsive" RowStyle-VerticalAlign="Bottom" OnRowDataBound="GvDirectors_RowDataBound" ForeColor="Black" BorderColor="Transparent"
                            OnRowDeleting="GvDirectors_RowDeleting" Font-Size="Small" RowStyle-Wrap="False">
                            <Columns>
                                <asp:CommandField DeleteText="X" ShowDeleteButton="True" ShowHeader="True">
                                    <ControlStyle BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" CssClass="btn" ForeColor="Red" BorderWidth="0px"
                                        Font-Bold="True" Font-Names="Lucida Handwriting" Font-Size="Medium" />
                                    <ItemStyle Wrap="False" />
                                </asp:CommandField>
                            </Columns>
                            <HeaderStyle BackColor="White" />
                            <RowStyle VerticalAlign="Middle" Wrap="False" BackColor="White" />
                            <SelectedRowStyle BackColor="#ADD8E6" />
                        </asp:GridView>
                        <asp:Label ID="lblNoDirectors" runat="server" Text="<span style=&quot;color: BlanchedAlmond&quot;>None assigned.</span>" Visible="false"></asp:Label>
                        <div>
                            <asp:CustomValidator ID="cvIsDirectorAssigned" runat="server" ErrorMessage="A director is required." CssClass="text-danger"
                                Display="Dynamic" EnableClientScript="False" OnServerValidate="CvIsDirectorAssigned_ServerValidate"></asp:CustomValidator>
                        </div>
                    </div>
                    <!-- Search for directors -->
                    <asp:Panel ID="pnlDirectorsSearch" runat="server" Visible="false">
                        <div class="col-xs-3" style="margin-top: 2px">
                            <asp:TextBox ID="txtSearchDirector" runat="server" placeholder="Directors search" Width="150px" Height="32px"></asp:TextBox>
                            <asp:Button ID="btnDirectorSearch" runat="server" Text="Search" CssClass="btn btn-sm" OnClick="BtnDirectorSearch_Click" CausesValidation="False" />
                        </div>
                        <div class="col-xs-3" style="margin-top: 10px">
                            <asp:DropDownList ID="ddlDirectorsSearchResult" runat="server" CssClass="dropdown input-sm" Height="25px" ToolTip="Add director"
                                AutoPostBack="True" Visible="False" OnSelectedIndexChanged="DdlDirectorsSearchResult_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="lblDirectorSearchResultMessage" runat="server" CssClass="label-info" Visible="False" BackColor="Transparent"></asp:Label>
                        </div>
                    </asp:Panel>
                </div>
            </asp:Panel>
            <!-- Add cast member -->
            <asp:Panel ID="pnlCast" runat="server" Visible=" false">
                <hr />
                <div class="form-group">
                    <div class="col-xs-1">
                        Cast
                    </div>
                    <div class="col-xs-5" style="margin-top: 2px">
                        <asp:GridView ID="gvCast" runat="server" CssClass="table-responsive" RowStyle-VerticalAlign="Bottom" BackColor="White" ForeColor="Black" BorderColor="Transparent"
                            OnRowDeleting="GvCast_RowDeleting" Font-Size="Small" RowStyle-Wrap="False" AutoGenerateColumns="False" OnRowDataBound="GvCast_RowDataBound">
                            <Columns>
                                <asp:CommandField DeleteText="X" ShowDeleteButton="True" ShowHeader="True">
                                    <ControlStyle BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" CssClass="btn btn-sm" ForeColor="Red" BorderWidth="0px" Font-Bold="True"
                                        Font-Names="Lucida Handwriting" Font-Size="Medium" />
                                    <ItemStyle Wrap="False" />
                                </asp:CommandField>
                                <asp:BoundField DataField="PERSONID" HeaderText="PERSONID" />
                                <asp:BoundField HeaderText="NAME" DataField="NAME" />
                                <asp:TemplateField HeaderText="ROLE">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRole" runat="server" Text='<%# Bind("ROLE") %>' MaxLength="100" OnTextChanged="TxtRole_TextChanged"></asp:TextBox>
                                        <div>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtRole" ErrorMessage="Role is required." CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ValidationGroup="RoleNames"></asp:RequiredFieldValidator>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="STATUS" HeaderText="STATUS" />
                            </Columns>
                            <HeaderStyle BackColor="White" />
                            <RowStyle VerticalAlign="Middle" Wrap="False" />
                            <SelectedRowStyle BackColor="#ADD8E6" />
                        </asp:GridView>
                        <asp:Label ID="lblNoCast" runat="server" Text="<span style=&quot;color: BlanchedAlmond&quot;>None assigned.</span>" Visible="false"></asp:Label>
                    </div>
                    <!-- Search for cast members -->
                    <asp:Panel ID="pnlCastSearch" runat="server" Visible="false">
                        <div class="col-xs-3" style="margin-top: 2px">
                            <asp:TextBox ID="txtSearchCast" runat="server" placeholder="Cast search" Width="150px" Height="32px"></asp:TextBox>
                            <asp:Button ID="btnCastSearch" runat="server" Text="Search" CssClass="btn btn-sm" OnClick="BtnCastSearch_Click" CausesValidation="False" />
                        </div>
                        <div class="col-xs-3" style="margin-top: 10px">
                            <asp:DropDownList ID="ddlCastSearchResult" runat="server" CssClass="dropdown input-sm" Height="25px" ToolTip="Add Cast"
                                AutoPostBack="True" Visible="False" OnSelectedIndexChanged="DdlCastSearchResult_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="lblCastSearchResultMessage" runat="server" CssClass="label-info" Visible="False" BackColor="Transparent"></asp:Label>
                        </div>
                    </asp:Panel>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlBtnModify" runat="server" Visible="false">
                <hr />
                <div class="form-group">
                    <div class="col-xs-offset-1 col-xs-1">
                        <asp:Button ID="btnModifyMovie" runat="server" CssClass="btn btn-sm" Text="Modify" OnClick="BtnModifyMovie_Click" />
                    </div>
                    <div class="col-xs-9">
                        <asp:Label ID="lblModifyMovieMessage" runat="server" CssClass="label-info" Visible="false" BackColor="Transparent"></asp:Label>
                    </div>
                </div>
            </asp:Panel>
            <div class="form-group">
                <div class="col-xs-1">
                    <asp:Button ID="btnUploadPoster" runat="server" OnClick="BtnUploadPoster_Click" CausesValidation="True" ValidationGroup="UploadPoster" Style="display: none" />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
