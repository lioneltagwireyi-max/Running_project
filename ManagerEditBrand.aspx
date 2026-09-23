<%@ Page Title="Edit Brand" Language="C#" MasterPageFile="~/Phonefit.Master" AutoEventWireup="true" CodeBehind="ManagerEditBrand.aspx.cs" Inherits="PhoneFit.ManagerEditBrand" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .brand-form {
            max-width: 700px;
            margin: 0 auto;
            padding: 24px;
            background: var(--paper);
            border: 1px solid var(--rule);
            border-radius: var(--r);
        }

        .form-field {
            margin-bottom: 18px;
        }

        .form-field label {
            display: block;
            margin-bottom: 6px;
            font-family: var(--ff-mono);
            font-size: 11px;
            text-transform: uppercase;
            letter-spacing: 0.06em;
            color: var(--fg-mute);
        }

        .form-field input,
        .form-field textarea {
            width: 100%;
            padding: 11px 13px;
            border: 1px solid var(--rule-strong);
            border-radius: var(--r-sm);
            background: var(--paper);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main">
        <section class="page-head">
            <div class="container">
                <div class="crumbs">
                    <a href="ManagerBrands.aspx">Brand Management</a>
                    <span class="sep">›</span>
                    <span>Edit Brand</span>
                </div>
                <h1>Edit Brand</h1>
                <p>
                    Update the selected smartphone brand information.
                </p>
            </div>
        </section>
        <section class="section">
            <div class="container">
                <div class="brand-form">
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red">
                    </asp:Label>
                    <div class="form-field">
                        <label>Brand Name *</label>
                        <asp:TextBox
                            ID="txtBrandName"
                            runat="server">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator
                            ID="rfvBrandName"
                            runat="server"
                            ControlToValidate="txtBrandName"
                            ErrorMessage="Brand name is required."
                            ForeColor="Red"
                            ValidationGroup="EditBrandGroup">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="form-field">
                        <label>Brand Description</label>
                        <asp:TextBox
                            ID="txtBrandDescription"
                            runat="server"
                            TextMode="MultiLine"
                            Rows="4">
                        </asp:TextBox>
                    </div>
                    <asp:Button
                        ID="btnSaveChanges"
                        runat="server"
                        Text="Save Changes"
                        CssClass="btn btn--indigo"
                        ValidationGroup="EditBrandGroup"
                        OnClick="btnSaveChanges_Click" />
                    <a href="ManagerBrands.aspx" style="margin-left: 12px;">
                        Back to Brand Management
                    </a>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
