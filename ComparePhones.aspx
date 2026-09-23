<%@ Page Title="" Language="C#" MasterPageFile="~/Phonefit.Master" AutoEventWireup="true" CodeBehind="ComparePhones.aspx.cs" Inherits="PhoneFit.ComparePhones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .compare-selectors {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 20px;
            margin-bottom: 20px;
        }

        .compare-field label {
            display: block;
            margin-bottom: 6px;
            font-family: var(--ff-mono);
            font-size: 11px;
            text-transform: uppercase;
            color: var(--fg-mute);
        }

        .compare-field select {
            width: 100%;
            padding: 11px 13px;
            border: 1px solid var(--rule-strong);
            border-radius: var(--r-sm);
            background: var(--paper);
        }

        .compare-actions {
            margin-bottom: 30px;
        }

        .compare-grid {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 20px;
        }

        .compare-card {
            padding: 24px;
            background: var(--paper);
            border: 1px solid var(--rule);
            border-radius: var(--r);
        }

        .compare-card h2 {
            margin-bottom: 6px;
        }

        .compare-brand {
            margin-bottom: 6px;
            font-family: var(--ff-mono);
            font-size: 11px;
            text-transform: uppercase;
            color: var(--fg-mute);
        }

        .compare-price {
            margin-bottom: 20px;
            font-family: var(--ff-display);
            font-size: 22px;
            font-weight: 700;
            color: var(--ink);
        }

        .compare-row {
            display: grid;
            grid-template-columns: 150px 1fr;
            gap: 14px;
            padding: 11px 0;
            border-bottom: 1px solid var(--rule);
        }

        .compare-label {
            font-family: var(--ff-mono);
            font-size: 11px;
            text-transform: uppercase;
            color: var(--fg-mute);
        }

        .compare-value {
            color: var(--fg-soft);
        }

        @media (max-width: 760px) {
            .compare-selectors,
            .compare-grid {
                grid-template-columns: 1fr;
            }

            .compare-row {
                grid-template-columns: 1fr;
                gap: 4px;
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main">

        <section class="page-head">
            <div class="container">
                <div class="crumbs">
                    <a href="Home.aspx">Home</a>
                    <span class="sep">›</span>
                    <a href="shop.aspx">Shop Phones</a>
                    <span class="sep">›</span>
                    <span>Compare Phones</span>
                </div>

                <h1>Compare Smartphones</h1>
                <p>
                    Select two smartphones to review their prices and specifications side by side.
                </p>
            </div>
        </section>

        <section class="section">
            <div class="container">

                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

                <div class="compare-selectors">
                    <div class="compare-field">
                        <label>Phone 1</label>
                        <asp:DropDownList ID="ddlPhone1" runat="server"></asp:DropDownList>
                    </div>

                    <div class="compare-field">
                        <label>Phone 2</label>
                        <asp:DropDownList ID="ddlPhone2" runat="server"></asp:DropDownList>
                    </div>
                </div>

                <div class="compare-actions">
                    <asp:Button
                        ID="btnCompare"
                        runat="server"
                        Text="Compare Phones"
                        CssClass="btn btn--indigo"
                        OnClick="btnCompare_Click" />
                </div>

                <asp:Panel ID="pnlComparison" runat="server" Visible="false">
                    <div class="compare-grid">

                        <article class="compare-card">
                            <div class="compare-brand">
                                <asp:Label ID="lblBrand1" runat="server"></asp:Label>
                            </div>

                            <h2>
                                <asp:Label ID="lblModel1" runat="server"></asp:Label>
                            </h2>

                            <div class="compare-price">
                                From R<asp:Label ID="lblPrice1" runat="server"></asp:Label>
                            </div>

                            <div class="compare-row">
                                <div class="compare-label">Processor</div>
                                <div class="compare-value"><asp:Label ID="lblProcessor1" runat="server"></asp:Label></div>
                            </div>

                            <div class="compare-row">
                                <div class="compare-label">Display</div>
                                <div class="compare-value"><asp:Label ID="lblDisplay1" runat="server"></asp:Label></div>
                            </div>

                            <div class="compare-row">
                                <div class="compare-label">Refresh Rate</div>
                                <div class="compare-value"><asp:Label ID="lblRefreshRate1" runat="server"></asp:Label></div>
                            </div>

                            <div class="compare-row">
                                <div class="compare-label">Battery</div>
                                <div class="compare-value"><asp:Label ID="lblBattery1" runat="server"></asp:Label></div>
                            </div>

                            <div class="compare-row">
                                <div class="compare-label">Rear Camera</div>
                                <div class="compare-value"><asp:Label ID="lblRearCamera1" runat="server"></asp:Label></div>
                            </div>

                            <div class="compare-row">
                                <div class="compare-label">Front Camera</div>
                                <div class="compare-value"><asp:Label ID="lblFrontCamera1" runat="server"></asp:Label></div>
                            </div>

                            <div class="compare-row">
                                <div class="compare-label">5G</div>
                                <div class="compare-value"><asp:Label ID="lbl5G1" runat="server"></asp:Label></div>
                            </div>

                            <div class="compare-row">
                                <div class="compare-label">Dual SIM</div>
                                <div class="compare-value"><asp:Label ID="lblDualSIM1" runat="server"></asp:Label></div>
                            </div>

                            <div class="compare-row">
                                <div class="compare-label">Expandable Storage</div>
                                <div class="compare-value"><asp:Label ID="lblExpandable1" runat="server"></asp:Label></div>
                            </div>

                            <div class="compare-row">
                                <div class="compare-label">Water Resistance</div>
                                <div class="compare-value"><asp:Label ID="lblWater1" runat="server"></asp:Label></div>
                            </div>
                        </article>


                        <article class="compare-card">
                            <div class="compare-brand">
                                <asp:Label ID="lblBrand2" runat="server"></asp:Label>
                            </div>

                            <h2>
                                <asp:Label ID="lblModel2" runat="server"></asp:Label>
                            </h2>

                            <div class="compare-price">
                                From R<asp:Label ID="lblPrice2" runat="server"></asp:Label>
                            </div>

                            <div class="compare-row">
                                <div class="compare-label">Processor</div>
                                <div class="compare-value"><asp:Label ID="lblProcessor2" runat="server"></asp:Label></div>
                            </div>

                            <div class="compare-row">
                                <div class="compare-label">Display</div>
                                <div class="compare-value"><asp:Label ID="lblDisplay2" runat="server"></asp:Label></div>
                            </div>

                            <div class="compare-row">
                                <div class="compare-label">Refresh Rate</div>
                                <div class="compare-value"><asp:Label ID="lblRefreshRate2" runat="server"></asp:Label></div>
                            </div>

                            <div class="compare-row">
                                <div class="compare-label">Battery</div>
                                <div class="compare-value"><asp:Label ID="lblBattery2" runat="server"></asp:Label></div>
                            </div>

                            <div class="compare-row">
                                <div class="compare-label">Rear Camera</div>
                                <div class="compare-value"><asp:Label ID="lblRearCamera2" runat="server"></asp:Label></div>
                            </div>

                            <div class="compare-row">
                                <div class="compare-label">Front Camera</div>
                                <div class="compare-value"><asp:Label ID="lblFrontCamera2" runat="server"></asp:Label></div>
                            </div>

                            <div class="compare-row">
                                <div class="compare-label">5G</div>
                                <div class="compare-value"><asp:Label ID="lbl5G2" runat="server"></asp:Label></div>
                            </div>

                            <div class="compare-row">
                                <div class="compare-label">Dual SIM</div>
                                <div class="compare-value"><asp:Label ID="lblDualSIM2" runat="server"></asp:Label></div>
                            </div>

                            <div class="compare-row">
                                <div class="compare-label">Expandable Storage</div>
                                <div class="compare-value"><asp:Label ID="lblExpandable2" runat="server"></asp:Label></div>
                            </div>

                            <div class="compare-row">
                                <div class="compare-label">Water Resistance</div>
                                <div class="compare-value"><asp:Label ID="lblWater2" runat="server"></asp:Label></div>
                            </div>
                        </article>

                    </div>
                </asp:Panel>

            </div>
        </section>
    </main>

</asp:Content>
