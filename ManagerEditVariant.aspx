<%@ Page Title="" Language="C#" MasterPageFile="~/Phonefit.Master" AutoEventWireup="true" CodeBehind="ManagerEditVariant.aspx.cs" Inherits="PhoneFit.ManagerEditVariant" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .variant-form {
            max-width: 850px;
            margin: 0 auto;
            padding: 24px;
            background: var(--paper);
            border: 1px solid var(--rule);
            border-radius: var(--r);
        }

        .form-row {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 18px;
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

        .form-field input {
            width: 100%;
            padding: 11px 13px;
            border: 1px solid var(--rule-strong);
            border-radius: var(--r-sm);
            background: var(--paper);
        }

        @media (max-width: 650px) {
            .form-row {
                grid-template-columns: 1fr;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main">

        <section class="page-head">
            <div class="container">
                <div class="crumbs">
                    <a href="ManagerProducts.aspx">Product Management</a>
                    <span class="sep">›</span>
                    <span>Edit Variant</span>
                </div>

                <h1>Edit Variant</h1>
                <p>Update the selected smartphone variant configuration, price and stock information.</p>
            </div>
        </section>

        <section class="section">
            <div class="container">
                <div class="variant-form">

                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

                    <div class="form-row">
                        <div class="form-field">
                            <label>RAM (GB) *</label>
                            <asp:TextBox ID="txtRAM" runat="server" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRAM" runat="server" ControlToValidate="txtRAM" ErrorMessage="RAM is required." ForeColor="Red" ValidationGroup="EditVariantGroup"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="rvRAM" runat="server" ControlToValidate="txtRAM" Type="Integer" MinimumValue="1" MaximumValue="128" ErrorMessage="RAM must be greater than 0." ForeColor="Red" ValidationGroup="EditVariantGroup"></asp:RangeValidator>
                        </div>

                        <div class="form-field">
                            <label>Storage (GB) *</label>
                            <asp:TextBox ID="txtStorage" runat="server" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvStorage" runat="server" ControlToValidate="txtStorage" ErrorMessage="Storage is required." ForeColor="Red" ValidationGroup="EditVariantGroup"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="rvStorage" runat="server" ControlToValidate="txtStorage" Type="Integer" MinimumValue="1" MaximumValue="4096" ErrorMessage="Storage must be greater than 0." ForeColor="Red" ValidationGroup="EditVariantGroup"></asp:RangeValidator>
                        </div>
                    </div>

                    <div class="form-field">
                        <label>Colour *</label>
                        <asp:TextBox ID="txtColour" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvColour" runat="server" ControlToValidate="txtColour" ErrorMessage="Colour is required." ForeColor="Red" ValidationGroup="EditVariantGroup"></asp:RequiredFieldValidator>
                    </div>

                    <div class="form-row">
                        <div class="form-field">
                            <label>Price (R) *</label>
                            <asp:TextBox ID="txtPrice" runat="server" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ControlToValidate="txtPrice" ErrorMessage="Price is required." ForeColor="Red" ValidationGroup="EditVariantGroup"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-field">
                            <label>Stock Quantity *</label>
                            <asp:TextBox ID="txtStockQuantity" runat="server" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvStockQuantity" runat="server" ControlToValidate="txtStockQuantity" ErrorMessage="Stock quantity is required." ForeColor="Red" ValidationGroup="EditVariantGroup"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="rvStockQuantity" runat="server" ControlToValidate="txtStockQuantity" Type="Integer" MinimumValue="0" MaximumValue="100000" ErrorMessage="Stock quantity cannot be negative." ForeColor="Red" ValidationGroup="EditVariantGroup"></asp:RangeValidator>
                        </div>
                    </div>

                    <div class="form-field">
                        <label>Low Stock Level *</label>
                        <asp:TextBox ID="txtLowStockLevel" runat="server" TextMode="Number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLowStockLevel" runat="server" ControlToValidate="txtLowStockLevel" ErrorMessage="Low stock level is required." ForeColor="Red" ValidationGroup="EditVariantGroup"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="rvLowStockLevel" runat="server" ControlToValidate="txtLowStockLevel" Type="Integer" MinimumValue="0" MaximumValue="100000" ErrorMessage="Low stock level cannot be negative." ForeColor="Red" ValidationGroup="EditVariantGroup"></asp:RangeValidator>
                    </div>

                    <asp:Button
                        ID="btnSaveChanges"
                        runat="server"
                        Text="Save Changes"
                        CssClass="btn btn--indigo"
                        ValidationGroup="EditVariantGroup"
                        OnClick="btnSaveChanges_Click" />

                    <asp:HyperLink
                        ID="lnkBackToVariants"
                        runat="server"
                        Text="Back to Variant Management"
                        Style="margin-left:12px;">
                    </asp:HyperLink>

                </div>
            </div>
        </section>

    </main>
</asp:Content>
