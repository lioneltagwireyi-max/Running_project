<%@ Page Title="Edit Product" Language="C#" MasterPageFile="~/Phonefit.Master" AutoEventWireup="true" CodeBehind="ManagerEditProduct.aspx.cs" Inherits="PhoneFit.ManagerEditProduct" %>

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
                    <span>Edit Product</span>
                </div>

                <h1>Edit Smartphone</h1>
                <p>Update the phone information and specifications stored in the PhoneFit catalogue.</p>
            </div>
        </section>

        <section class="section">
            <div class="container">
                <div class="product-form">

                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

                    <div class="form-section">
                        <h2>1. Phone Information</h2>

                        <div class="form-field">
                            <label>Brand</label>
                            <asp:DropDownList ID="ddlBrand" runat="server"></asp:DropDownList>
                        </div>

                        <div class="form-field">
                            <label>Model Name</label>
                            <asp:TextBox ID="txtModelName" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-row">
                            <div class="form-field">
                                <label>Operating System</label>
                                <asp:TextBox ID="txtOperatingSystem" runat="server"></asp:TextBox>
                            </div>

                            <div class="form-field">
                                <label>Release Year</label>
                                <asp:TextBox ID="txtReleaseYear" runat="server" TextMode="Number"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-field">
                            <label>Description</label>
                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
                        </div>

                        <div class="form-field">
                            <label>Image URL</label>
                            <asp:TextBox ID="txtImagePath" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-section">
                        <h2>2. Phone Specifications</h2>

                        <div class="form-field">
                            <label>Processor</label>
                            <asp:TextBox ID="txtProcessor" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-row">
                            <div class="form-field">
                                <label>Screen Size (Inches)</label>
                                <asp:TextBox ID="txtScreenSize" runat="server" TextMode="Number"></asp:TextBox>
                            </div>

                            <div class="form-field">
                                <label>Screen Type</label>
                                <asp:TextBox ID="txtScreenType" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-row">
                            <div class="form-field">
                                <label>Refresh Rate (Hz)</label>
                                <asp:TextBox ID="txtRefreshRate" runat="server" TextMode="Number"></asp:TextBox>
                            </div>

                            <div class="form-field">
                                <label>Battery Capacity (mAh)</label>
                                <asp:TextBox ID="txtBatteryCapacity" runat="server" TextMode="Number"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-row">
                            <div class="form-field">
                                <label>Rear Camera (MP)</label>
                                <asp:TextBox ID="txtRearCameraMP" runat="server" TextMode="Number"></asp:TextBox>
                            </div>

                            <div class="form-field">
                                <label>Front Camera (MP)</label>
                                <asp:TextBox ID="txtFrontCameraMP" runat="server" TextMode="Number"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-field">
                            <label>Water Resistance</label>
                            <asp:TextBox ID="txtWaterResistance" runat="server"></asp:TextBox>
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
                            <asp:CheckBox ID="chkExpandableStorage" runat="server" Text="Expandable Storage" />
                        </div>
                    </div>

                    <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" CssClass="btn btn--indigo" OnClick="btnSaveChanges_Click" />
                    <a href="ManagerProducts.aspx">← Back to Product Management</a>

                </div>
            </div>
        </section>

    </main>
</asp:Content>
