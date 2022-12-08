<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageAccount.aspx.cs" Inherits="ReelflicsWebsite.Member.ManageAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <div style="margin-top: 20px">
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="label-info" Visible="False" Height="40px" BackColor="Transparent"></asp:Label>
        </div>
        <h4 style="text-decoration: underline">Manage Your Account</h4>
        <asp:Label ID="lblUpdateSuccessMessage" runat="server" Visible="false"></asp:Label>
        <asp:Panel ID="pnlMemberAccountInformation" runat="server">
            <br />
            <div class="form-group">
                <asp:Label runat="server" Text="Username" AssociatedControlID="litUsername" CssClass="col-xs-2 control-label"></asp:Label>
                <div class="col-xs-3" style="margin-top: 10px">
                    <asp:Literal ID="litUsername" runat="server"></asp:Literal>
                </div>
                <asp:Label runat="server" Text="Pseudonym" AssociatedControlID="litPseudonym" CssClass="col-xs-2 control-label"></asp:Label>
                <div class="col-xs-3" style="margin-top: 10px">
                    <asp:Literal ID="litPseudonym" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtFirstName" CssClass="col-xs-2 control-label">First name</asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control input-sm" MaxLength="15" ToolTip="First name" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFirstName" ErrorMessage="First name is required."
                        CssClass="text-danger" Display="Dynamic" EnableClientScript="False" />
                </div>
                <asp:Label runat="server" AssociatedControlID="txtLastName" CssClass="col-xs-2 control-label">Last name</asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control input-sm" MaxLength="20" ToolTip="Last name" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLastName" ErrorMessage="Last name is required."
                        CssClass="text-danger" Display="Dynamic" EnableClientScript="False" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Email" AssociatedControlID="txtEmail" CssClass="col-xs-2 control-label"></asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control input-sm" MaxLength="25" ToolTip="Email address" TextMode="Email" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required."
                        CssClass="text-danger" Display="Dynamic" EnableClientScript="False" />
                    <asp:CustomValidator ID="cvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="The email already exists." CssClass="text-danger"
                        Display="Dynamic" EnableClientScript="False" OnServerValidate="CvEmail_ServerValidate"></asp:CustomValidator>
                </div>
                <asp:Label runat="server" Text="Occupation" AssociatedControlID="txtOccupation" CssClass="col-xs-2 control-label"></asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox runat="server" ID="txtOccupation" CssClass="form-control input-sm" MaxLength="25" ToolTip="Occupation" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOccupation" ErrorMessage="Occupation is required."
                        CssClass="text-danger" Display="Dynamic" EnableClientScript="False" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ddlGender" CssClass="col-xs-2 control-label">Gender</asp:Label>
                <div class="col-xs-3">
                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="dropdown input-sm" Height="25px" Width="90px" ToolTip="Gender">
                        <asp:ListItem Value="M">Male</asp:ListItem>
                        <asp:ListItem Value="F">Female</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <asp:Label runat="server" Text="Birthdate" AssociatedControlID="txtBirthdate" CssClass="col-xs-2 control-label"></asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox runat="server" ID="txtBirthdate" CssClass="form-control input-sm" MaxLength="11" placeholder="dd-MMM-yyyy"
                        ToolTip="Birthdate" Width="110px"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBirthdate" ErrorMessage="Birthdate is required."
                        CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvBirthdate" runat="server" ControlToValidate="txtBirthdate" ErrorMessage="Enter a valid date."
                        CssClass="text-danger" Display="Dynamic" EnableClientScript="False" OnServerValidate="CvBirthdate_ServerValidate"></asp:CustomValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Phone number" AssociatedControlID="txtPhoneNumber" CssClass="col-xs-2 control-label"></asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox runat="server" ID="txtPhoneNumber" CssClass="form-control input-sm" MaxLength="8" ToolTip="Phone number" Width="80px" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPhoneNumber" ErrorMessage="Phone number is required." CssClass="text-danger"
                        Display="Dynamic" EnableClientScript="False" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtPhoneNumber" ValidationExpression="\d{8}"
                        ErrorMessage="Enter exactly 8 digits only." CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RegularExpressionValidator>
                </div>
                <asp:Label runat="server" Text="Education level" AssociatedControlID="ddlEducationLevel" CssClass="col-xs-2 control-label"></asp:Label>
                <div class="col-xs-3">
                    <asp:DropDownList ID="ddlEducationLevel" runat="server" CssClass="dropdown input-sm" Height="25px" Width="120px" ToolTip="Education level">
                        <asp:ListItem Value="none selected">-- Select --</asp:ListItem>
                        <asp:ListItem>none</asp:ListItem>
                        <asp:ListItem>primary</asp:ListItem>
                        <asp:ListItem>secondary</asp:ListItem>
                        <asp:ListItem>tertiary</asp:ListItem>
                        <asp:ListItem>post tertiary</asp:ListItem>
                    </asp:DropDownList>
                    <div>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlEducationLevel" ErrorMessage="Education level is required." CssClass="text-danger"
                            Display="Dynamic" EnableClientScript="False" InitialValue="none selected" ToolTip="Education level"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <hr />
            <!-- Credit card -->
            <h6>Credit Card Information</h6>
            <div class="form-group">
                <asp:Label runat="server" Text="Cardholder name" AssociatedControlID="txtCardHolderName" CssClass="col-xs-2 control-label"></asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox runat="server" ID="txtCardHolderName" CssClass="form-control input-sm" MaxLength="35" ToolTip="Cardholder name"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCardHolderName" ErrorMessage="Cardholder name is required." CssClass="text-danger"
                        Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
                </div>
                <asp:Label runat="server" Text="Card number" AssociatedControlID="txtCardNumber" CssClass="col-xs-2 control-label"></asp:Label>
                <div class="col-xs-3">
                    <asp:TextBox runat="server" ID="txtCardNumber" CssClass="form-control input-sm" MaxLength="16" ToolTip="Credit card number"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCardNumber" ErrorMessage="Card number is required." CssClass="text-danger"
                        Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtCardNumber" ValidationExpression="^\d{15,16}$"
                        ErrorMessage="Enter exactly 15 or 16 digits only." CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Card type" AssociatedControlID="ddlCardType" CssClass="col-xs-2 control-label"></asp:Label>
                <div class="col-xs-2">
                    <asp:DropDownList ID="ddlCardType" runat="server" CssClass="dropdown input-sm" Height="25px" ToolTip="Card type">
                        <asp:ListItem Value="none selected">-- Select --</asp:ListItem>
                        <asp:ListItem>American Express</asp:ListItem>
                        <asp:ListItem>MasterCard</asp:ListItem>
                        <asp:ListItem>UnionPay</asp:ListItem>
                        <asp:ListItem>Visa</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlCardType" ErrorMessage="Card type is required." CssClass="text-danger"
                        Display="Dynamic" EnableClientScript="False" InitialValue="none selected"></asp:RequiredFieldValidator>
                </div>
                <asp:Label runat="server" Text="Security code/CVV" AssociatedControlID="txtSecurityCode" CssClass="col-xs-2 control-label"></asp:Label>
                <div class="col-xs-1">
                    <asp:TextBox runat="server" ID="txtSecurityCode" CssClass="form-control input-sm" MaxLength="4" ToolTip="CVC/CVV"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSecurityCode" ErrorMessage="Security code is required." CssClass="text-danger"
                        Display="Dynamic" EnableClientScript="False" />
                    <asp:RegularExpressionValidator ID="revSecurityCode" runat="server" ControlToValidate="txtSecurityCode" ValidationExpression="^\d{1,4}$"
                        ErrorMessage="Enter digits only." CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RegularExpressionValidator>
                    <asp:CustomValidator ID="cvSecurityCode" runat="server" ControlToValidate="txtSecurityCode"  CssClass="text-danger" 
                        Display="Dynamic" EnableClientScript="False" OnServerValidate="CvSecurityCode_ServerValidate"></asp:CustomValidator>
                </div>
                <asp:Label runat="server" Text="Expires" AssociatedControlID="ddlExpiryMonth" CssClass="col-xs-1 control-label"></asp:Label>
                <div class="col-xs-4">
                    <asp:DropDownList ID="ddlExpiryMonth" runat="server" CssClass="dropdown input-sm" Height="25px" ToolTip="Expiry month">
                        <asp:ListItem Value="none selected">Month</asp:ListItem>
                        <asp:ListItem>01</asp:ListItem>
                        <asp:ListItem>02</asp:ListItem>
                        <asp:ListItem>03</asp:ListItem>
                        <asp:ListItem>04</asp:ListItem>
                        <asp:ListItem>05</asp:ListItem>
                        <asp:ListItem>06</asp:ListItem>
                        <asp:ListItem>07</asp:ListItem>
                        <asp:ListItem>08</asp:ListItem>
                        <asp:ListItem>09</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList> /
                    <asp:DropDownList ID="ddlExpiryYear" runat="server" CssClass="dropdown input-sm" Height="25px" ToolTip="Expiry year"></asp:DropDownList>
                    <div>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlExpiryMonth" ErrorMessage="Month is required." CssClass="text-danger"
                            Display="Dynamic" EnableClientScript="False" InitialValue="none selected">
                        </asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="cvExpiryDate" runat="server" ControlToValidate="ddlExpiryMonth" ErrorMessage="Credit card is expired." CssClass="text-danger"
                            Display="Dynamic" EnableClientScript="False" OnServerValidate="CvExpiryDate_ServerValidate">
                        </asp:CustomValidator>
                    </div>
                </div>
            </div>
            <hr />
            <div class="form-group">
                <div class="col-xs-offset-2 col-xs-1">
                    <asp:Button runat="server" ID="btnUpdate" Text="Update" CssClass="btn btn-sm" OnClick="BtnUpdate_Click" />
                </div>
                <div class="col-xs-9">
                    <asp:Label ID="lblUpdateMessage" runat="server" CssClass="label-info" Visible="false" BackColor="Transparent"></asp:Label>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
