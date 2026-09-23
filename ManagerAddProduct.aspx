<%@ Page Title="Add Product" Language="C#" MasterPageFile="~/Phonefit.Master" AutoEventWireup="true" CodeBehind="ManagerAddProduct.aspx.cs" Inherits="PhoneFit.ManagerAddProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
    .product-form {
        max-width: 850px;
        margin: 0 auto;
    }

    .form-section {
        background: var(--paper);
        border: 1px solid var(--rule);
        border-radius: var(--r);
        padding: 24px;
        margin-bottom: 24px;
    }

    .form-section h2 {
        font-size: 20px;
        margin-bottom: 20px;
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
        font-family: var(--ff-mono);
        font-size: 11px;
        text-transform: uppercase;
        letter-spacing: 0.06em;
        color: var(--fg-mute);
        margin-bottom: 6px;
    }

    .form-field input,
    .form-field select,
    .form-field textarea {
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

    .checkbox-field {
    display: flex;
    align-items: center;
    min-height: 44px;
    margin-bottom: 18px;
}

.checkbox-field input {
    width: auto;
    margin: 0 10px 0 0;
}

.checkbox-field label {
    margin: 0;
    font-family: var(--ff-body);
    font-size: 14px;
    font-weight: 500;
    color: var(--fg-soft);
    text-transform: none;
    letter-spacing: normal;
    cursor: pointer;
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
                    <span>Add Product</span>
                </div>
                <h1>Add Smartphone</h1>
                <p>Add a new smartphone to the PhoneFit catalogue.</p>
            </div>
        </section>
        <section class="section">
            <div class="container">
                <div class="product-form">
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                    <div class="form-section">
                        <h2>1. Phone Information</h2>
                        <div class="form-field">
                            <label>Brand *</label>
                            <asp:DropDownList ID="ddlBrand" runat="server"></asp:DropDownList>
                        </div>
                        <div class="form-field">
                            <label>Model Name *</label>
                            <asp:TextBox ID="txtModelName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfvModelName"
                                runat="server"
                                ControlToValidate="txtModelName"
                                ErrorMessage="Model name is required."
                                ForeColor="Red"
                                ValidationGroup="ProductGroup">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="form-row">
                            <div class="form-field">
                                <label>Operating System *</label>
                                <asp:TextBox ID="txtOperatingSystem" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="rfvOperatingSystem"
                                    runat="server"
                                    ControlToValidate="txtOperatingSystem"
                                    ErrorMessage="Operating system is required."
                                    ForeColor="Red"
                                    ValidationGroup="ProductGroup">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-field">
                                <label>Release Year</label>
                                <asp:TextBox ID="txtReleaseYear" runat="server" TextMode="Number"></asp:TextBox>
                                <asp:RangeValidator
                                    ID="rvReleaseYear"
                                    runat="server"
                                    ControlToValidate="txtReleaseYear"
                                    Type="Integer"
                                    MinimumValue="1990"
                                    MaximumValue="2030"
                                    ErrorMessage="Release year must be between 1990 and 2030."
                                    ForeColor="Red"
                                    ValidationGroup="ProductGroup">
                                </asp:RangeValidator>
                            </div>
                        </div>
                        <div class="form-field">
                            <label>Description *</label>
                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfvDescription"
                                runat="server"
                                ControlToValidate="txtDescription"
                                ErrorMessage="Description is required."
                                ForeColor="Red"
                                ValidationGroup="ProductGroup">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="form-field">
                            <label>Image URL *</label>
                            <asp:TextBox ID="txtImagePath" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfvImagePath"
                                runat="server"
                                ControlToValidate="txtImagePath"
                                ErrorMessage="Image URL is required."
                                ForeColor="Red"
                                ValidationGroup="ProductGroup">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-section">
                        <h2>2. Phone Specifications</h2>
                        <div class="form-field">
                            <label>Processor *</label>
                            <asp:TextBox ID="txtProcessor" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfvProcessor"
                                runat="server"
                                ControlToValidate="txtProcessor"
                                ErrorMessage="Processor is required."
                                ForeColor="Red"
                                ValidationGroup="ProductGroup">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="form-row">
                            <div class="form-field">
                                <label>Screen Size (Inches)</label>
                                <asp:TextBox ID="txtScreenSize" runat="server" TextMode="Number" step="0.01"></asp:TextBox>
                            </div>
                            <div class="form-field">
                                <label>Screen Type *</label>
                                <asp:TextBox ID="txtScreenType" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="rfvScreenType"
                                    runat="server"
                                    ControlToValidate="txtScreenType"
                                    ErrorMessage="Screen type is required."
                                    ForeColor="Red"
                                    ValidationGroup="ProductGroup">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-field">
                                <label>Refresh Rate (Hz)</label>
                                <asp:TextBox ID="txtRefreshRate" runat="server" TextMode="Number"></asp:TextBox>
                                <asp:RangeValidator
                                    ID="rvRefreshRate"
                                    runat="server"
                                    ControlToValidate="txtRefreshRate"
                                    Type="Integer"
                                    MinimumValue="1"
                                    MaximumValue="1000"
                                    ErrorMessage="Enter a valid refresh rate."
                                    ForeColor="Red"
                                    ValidationGroup="ProductGroup">
                                </asp:RangeValidator>
                            </div>
                            <div class="form-field">
                                <label>Battery Capacity (mAh)</label>
                                <asp:TextBox ID="txtBatteryCapacity" runat="server" TextMode="Number">
                                </asp:TextBox>
                                <asp:RangeValidator
                                    ID="rvBatteryCapacity"
                                    runat="server"
                                    ControlToValidate="txtBatteryCapacity"
                                    Type="Integer"
                                    MinimumValue="1"
                                    MaximumValue="50000"
                                    ErrorMessage="Enter a valid battery capacity."
                                    ForeColor="Red"
                                    ValidationGroup="ProductGroup">
                                </asp:RangeValidator>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-field">
                                <label>Rear Camera (MP)</label>
                                <asp:TextBox ID="txtRearCameraMP" runat="server" TextMode="Number" step="0.01">
                                </asp:TextBox>
                            </div>
                            <div class="form-field">
                                <label>Front Camera (MP)</label>
                                <asp:TextBox ID="txtFrontCameraMP" runat="server" TextMode="Number" step="0.01">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="form-field">
                            <label>Water Resistance</label>
                            <asp:TextBox ID="txtWaterResistance" runat="server" placeholder="Example: IP68">
                            </asp:TextBox>
                        </div>
                    <div class="form-row">
                        <div class="checkbox-field">
                            <asp:CheckBox ID="chkSupports5G" runat="server" Text="Supports 5G" />
                        </div>
                        <div class="checkbox-field">
                            <asp:CheckBox ID="chkDualSIM" runat="server" Text="Dual SIM" />
                        </div>
                    </div>
                    <div class="checkbox-field">
                        <asp:CheckBox ID="chkExpandableStorage" runat="server"
                            Text="Expandable Storage" />
                    </div>
                    </div>
                    <div class="form-section">
                        <h2>3. Initial Variant</h2>
                        <div class="form-row">
                            <div class="form-field">
                                <label>RAM (GB) *</label>
                                <asp:TextBox ID="txtRAM" runat="server" TextMode="Number">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="rfvRAM"
                                    runat="server"
                                    ControlToValidate="txtRAM"
                                    ErrorMessage="RAM is required."
                                    ForeColor="Red"
                                    ValidationGroup="ProductGroup">
                                </asp:RequiredFieldValidator>
                                <asp:RangeValidator
                                ID="rvRAM"
                                runat="server"
                                ControlToValidate="txtRAM"
                                Type="Integer"
                                MinimumValue="1"
                                MaximumValue="128"
                                ErrorMessage="RAM must be greater than 0."
                                ForeColor="Red"
                                ValidationGroup="ProductGroup">
                            </asp:RangeValidator>
                            </div>
                            <div class="form-field">
                                <label>Storage (GB) *</label>
                                <asp:TextBox ID="txtStorage" runat="server" TextMode="Number">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="rfvStorage"
                                    runat="server"
                                    ControlToValidate="txtStorage"
                                    ErrorMessage="Storage is required."
                                    ForeColor="Red"
                                    ValidationGroup="ProductGroup">
                                </asp:RequiredFieldValidator>
                                <asp:RangeValidator
                                    ID="rvStorage"
                                    runat="server"
                                    ControlToValidate="txtStorage"
                                    Type="Integer"
                                    MinimumValue="1"
                                    MaximumValue="4096"
                                    ErrorMessage="Storage must be greater than 0."
                                    ForeColor="Red"
                                    ValidationGroup="ProductGroup">
                                </asp:RangeValidator>
                            </div>
                        </div>
                        <div class="form-field">
                            <label>Colour *</label>
                            <asp:TextBox ID="txtColour" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfvColour"
                                runat="server"
                                ControlToValidate="txtColour"
                                ErrorMessage="Colour is required."
                                ForeColor="Red"
                                ValidationGroup="ProductGroup">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="form-row">
                            <div class="form-field">
                                <label>Price (R) *</label>
                                <asp:TextBox ID="txtPrice" runat="server" TextMode="Number">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="rfvPrice"
                                    runat="server"
                                    ControlToValidate="txtPrice"
                                    ErrorMessage="Price is required."
                                    ForeColor="Red"
                                    ValidationGroup="ProductGroup">
                                </asp:RequiredFieldValidator>
                                <asp:RangeValidator
                                    ID="rvPrice"
                                    runat="server"
                                    ControlToValidate="txtPrice"
                                    Type="Currency"
                                    MinimumValue="0"
                                    MaximumValue="1000000"
                                    ErrorMessage="Enter a valid price."
                                    ForeColor="Red"
                                    ValidationGroup="ProductGroup">
                                </asp:RangeValidator>
                            </div>
                            <div class="form-field">
                                <label>Stock Quantity *</label>
                                <asp:TextBox ID="txtStockQuantity" runat="server" TextMode="Number">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="rfvStockQuantity"
                                    runat="server"
                                    ControlToValidate="txtStockQuantity"
                                    ErrorMessage="Stock quantity is required."
                                    ForeColor="Red"
                                    ValidationGroup="ProductGroup">
                                </asp:RequiredFieldValidator>
                                <asp:RangeValidator
                                    ID="rvStockQuantity"
                                    runat="server"
                                    ControlToValidate="txtStockQuantity"
                                    Type="Integer"
                                    MinimumValue="0"
                                    MaximumValue="100000"
                                    ErrorMessage="Stock quantity cannot be negative."
                                    ForeColor="Red"
                                    ValidationGroup="ProductGroup">
                                </asp:RangeValidator>
                            </div>
                        </div>
                        <div class="form-field">
                            <label>Low Stock Level *</label>
                            <asp:TextBox ID="txtLowStockLevel" runat="server" TextMode="Number">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfvLowStockLevel"
                                runat="server"
                                ControlToValidate="txtLowStockLevel"
                                ErrorMessage="Low stock level is required."
                                ForeColor="Red"
                                ValidationGroup="ProductGroup">
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator
                                ID="rvLowStockLevel"
                                runat="server"
                                ControlToValidate="txtLowStockLevel"
                                Type="Integer"
                                MinimumValue="0"
                                MaximumValue="100000"
                                ErrorMessage="Low stock level cannot be negative."
                                ForeColor="Red"
                                ValidationGroup="ProductGroup">
                            </asp:RangeValidator>
                        </div>
                    </div>
                    <asp:Button ID="btnSaveProduct" runat="server" Text="Add Product" CssClass="btn btn--indigo"
                    ValidationGroup="ProductGroup" OnClick="btnSaveProduct_Click" />
                    <a href="ManagerProducts.aspx">
                        ← Back to Product Management
                    </a>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
