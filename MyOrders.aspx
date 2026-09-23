<%@ Page Title="" Language="C#" MasterPageFile="~/Phonefit.Master" AutoEventWireup="true" CodeBehind="MyOrders.aspx.cs" Inherits="PhoneFit.MyOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .orders-list {
            display: grid;
            gap: 16px;
        }

        .order-card {
            display: grid;
            grid-template-columns: 1fr 180px 160px 180px auto;
            gap: 20px;
            align-items: center;
            padding: 20px 22px;
            background: var(--paper);
            border: 1px solid var(--rule);
            border-radius: var(--r);
        }

        .order-number {
            font-family: var(--ff-display);
            font-size: 18px;
            font-weight: 700;
            color: var(--ink);
        }

        .order-label {
            margin-bottom: 4px;
            font-family: var(--ff-mono);
            font-size: 10px;
            text-transform: uppercase;
            letter-spacing: 0.06em;
            color: var(--fg-mute);
        }

        .order-value {
            font-size: 14px;
            color: var(--fg-soft);
        }

        .order-total {
            font-family: var(--ff-display);
            font-weight: 700;
            color: var(--ink);
        }

        .status-badge {
            display: inline-block;
            padding: 5px 10px;
            border-radius: 999px;
            background: #dff7eb;
            color: #087443;
            font-size: 12px;
            font-weight: 600;
        }

        @media (max-width: 850px) {
            .order-card {
                grid-template-columns: 1fr 1fr;
            }
        }

        @media (max-width: 560px) {
            .order-card {
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
                    <a href="Home.aspx">Home</a>
                    <span class="sep">›</span>
                    <span>My Orders</span>
                </div>
                <h1>My Orders</h1>
                <p>View your previous PhoneFit orders and access their invoices.</p>
            </div>
        </section>
        <section class="section">
            <div class="container">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                <asp:Panel ID="pnlNoOrders" runat="server" Visible="false">
                    <div style="padding:50px 30px; text-align:center; background:var(--paper); border:1px solid var(--rule); border-radius:var(--r);">
                        <h2>No previous orders</h2>
                        <p>You have not placed a PhoneFit order yet.</p>
                        <a href="shop.aspx" class="btn btn--indigo">Shop Phones</a>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlOrders" runat="server" Visible="false">
                    <div class="orders-list">

                        <asp:Repeater ID="rptOrders" runat="server">
                            <ItemTemplate>

                                <article class="order-card">

                                    <div>
                                        <div class="order-label">Order</div>
                                        <div class="order-number">
                                            #<%# Eval("OrderID") %>
                                        </div>
                                    </div>

                                    <div>
                                        <div class="order-label">Date</div>
                                        <div class="order-value">
                                            <%# Eval("OrderDate", "{0:dd MMM yyyy HH:mm}") %>
                                        </div>
                                    </div>

                                    <div>
                                        <div class="order-label">Status</div>
                                        <div>
                                            <span class="status-badge">
                                                <%# Eval("OrderStatus") %>
                                            </span>
                                        </div>
                                    </div>

                                    <div>
                                        <div class="order-label">Total</div>
                                        <div class="order-total">
                                            R<%# Eval("TotalAmount", "{0:N2}") %>
                                        </div>
                                    </div>

                                    <div>
                                        <a href='<%# "Invoice.aspx?id=" + Eval("OrderID") %>'
                                           class="btn btn--ghost btn--sm">
                                            View Invoice
                                        </a>
                                    </div>
                                </article>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </asp:Panel>
            </div>
        </section>
    </main>
</asp:Content>
