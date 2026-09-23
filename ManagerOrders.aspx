<%@ Page Title="" Language="C#" MasterPageFile="~/Phonefit.Master" AutoEventWireup="true" CodeBehind="ManagerOrders.aspx.cs" Inherits="PhoneFit.ManagerOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .orders-table-wrapper {
            overflow-x: auto;
            background: var(--paper);
            border: 1px solid var(--rule);
            border-radius: var(--r);
        }

        .orders-table {
            width: 100%;
            border-collapse: collapse;
        }

        .orders-table th,
        .orders-table td {
            padding: 15px;
            text-align: left;
            border-bottom: 1px solid var(--rule);
        }

        .orders-table th {
            background: var(--bg);
            font-family: var(--ff-mono);
            font-size: 11px;
            text-transform: uppercase;
            letter-spacing: 0.06em;
            color: var(--fg-mute);
        }

        .orders-table td {
            font-size: 14px;
            color: var(--fg-soft);
        }

        .order-number {
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

        .order-total {
            font-family: var(--ff-display);
            font-weight: 700;
            color: var(--ink);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main">

        <section class="page-head">
            <div class="container">
                <div class="crumbs">
                    <a href="Manager.aspx">Manager</a>
                    <span class="sep">›</span>
                    <span>Orders & Transactions</span>
                </div>

                <h1>Orders & Transactions</h1>
                <p>View completed PhoneFit customer transactions and their details.</p>
            </div>
        </section>

        <section class="section">
            <div class="container">

                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

                <asp:Panel ID="pnlNoOrders" runat="server" Visible="false">
                    <div style="padding:50px 30px; text-align:center; background:var(--paper); border:1px solid var(--rule); border-radius:var(--r);">
                        <h2>No customer orders</h2>
                        <p>No PhoneFit customer transactions have been recorded yet.</p>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlOrders" runat="server" Visible="false">
                    <div class="orders-table-wrapper">
                        <table class="orders-table">
                            <thead>
                                <tr>
                                    <th>Order</th>
                                    <th>Customer</th>
                                    <th>Email</th>
                                    <th>Date</th>
                                    <th>Status</th>
                                    <th>Total</th>
                                    <th>Action</th>
                                </tr>
                            </thead>

                            <tbody>
                                <asp:Repeater ID="rptOrders" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <span class="order-number">
                                                    #<%# Eval("OrderID") %>
                                                </span>
                                            </td>

                                            <td>
                                                <%# Eval("CustomerName") %>
                                            </td>

                                            <td>
                                                <%# Eval("CustomerEmail") %>
                                            </td>

                                            <td>
                                                <%# Eval("OrderDate", "{0:dd MMM yyyy HH:mm}") %>
                                            </td>

                                            <td>
                                                <span class="status-badge">
                                                    <%# Eval("OrderStatus") %>
                                                </span>
                                            </td>

                                            <td>
                                                <span class="order-total">
                                                    R<%# Eval("TotalAmount", "{0:N2}") %>
                                                </span>
                                            </td>

                                            <td>
                                                <a href='<%# "ManagerOrderDetails.aspx?id=" + Eval("OrderID") %>'
                                                   class="btn btn--ghost btn--sm">
                                                    View Details
                                                </a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </asp:Panel>

            </div>
        </section>

    </main>

</asp:Content>
