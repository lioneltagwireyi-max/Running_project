<%@ Page Title="" Language="C#" MasterPageFile="~/Phonefit.Master" AutoEventWireup="true" CodeBehind="ManagerReports.aspx.cs" Inherits="PhoneFit.ManagerReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .report-grid {
            display: grid;
            grid-template-columns: repeat(3, 1fr);
            gap: 18px;
            margin-bottom: 34px;
        }

        .report-card {
            padding: 22px;
            background: var(--paper);
            border: 1px solid var(--rule);
            border-radius: var(--r);
        }

        .report-card .label {
            font-family: var(--ff-mono);
            font-size: 11px;
            text-transform: uppercase;
            letter-spacing: 0.07em;
            color: var(--fg-mute);
        }

        .report-card .value {
            margin-top: 10px;
            font-family: var(--ff-display);
            font-size: 28px;
            font-weight: 700;
            color: var(--ink);
        }

        .report-section {
            margin-top: 30px;
        }

        .report-section h2 {
            margin-bottom: 6px;
        }

        .report-section > p {
            margin-bottom: 18px;
            color: var(--fg-mute);
        }

        .report-table-wrapper {
            overflow-x: auto;
            background: var(--paper);
            border: 1px solid var(--rule);
            border-radius: var(--r);
        }

        .report-table {
            width: 100%;
            border-collapse: collapse;
        }

        .report-table th,
        .report-table td {
            padding: 14px 16px;
            text-align: left;
            border-bottom: 1px solid var(--rule);
        }

        .report-table th {
            background: var(--bg);
            font-family: var(--ff-mono);
            font-size: 11px;
            text-transform: uppercase;
            letter-spacing: 0.06em;
            color: var(--fg-mute);
        }

        .report-table td {
            font-size: 14px;
        }

        .report-number {
            font-family: var(--ff-display);
            font-weight: 700;
            color: var(--ink);
        }

        @media (max-width: 900px) {
            .report-grid {
                grid-template-columns: repeat(2, 1fr);
            }
        }

        @media (max-width: 560px) {
            .report-grid {
                grid-template-columns: 1fr;
            }
        }

        .chart-grid {
        display: grid;
        grid-template-columns: 1fr 1fr;
        gap: 20px;
        margin-top: 20px;
        margin-bottom: 30px;
    }

    .chart-card {
        padding: 22px;
        background: var(--paper);
        border: 1px solid var(--rule);
        border-radius: var(--r);
    }

    .chart-card h3 {
        margin-bottom: 6px;
    }

    .chart-card p {
        margin-bottom: 18px;
        color: var(--fg-mute);
    }

    .chart-container {
        position: relative;
        width: 100%;
        height: 360px;
    }

    @media (max-width: 900px) {
        .chart-grid {
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
                    <a href="Manager.aspx">Manager Dashboard</a>
                    <span class="sep">›</span>
                    <span>Reports</span>
                </div>

                <h1>Manager Reports</h1>
                <p>
                    Review PhoneFit sales, stock and customer registration information.
                </p>
            </div>
        </section>

        <section class="section">
            <div class="container">

                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

                <asp:HiddenField ID="hfProductLabels" runat="server" />
                <asp:HiddenField ID="hfProductValues" runat="server" />
                <asp:HiddenField ID="hfRegistrationLabels" runat="server" />
                <asp:HiddenField ID="hfRegistrationValues" runat="server" />

                <div class="report-grid">

                    <article class="report-card">
                        <div class="label">Total Revenue</div>
                        <div class="value">
                            <asp:Label ID="lblTotalRevenue" runat="server"></asp:Label>
                        </div>
                    </article>

                    <article class="report-card">
                        <div class="label">Total Orders</div>
                        <div class="value">
                            <asp:Label ID="lblTotalOrders" runat="server"></asp:Label>
                        </div>
                    </article>

                    <article class="report-card">
                        <div class="label">Total Units Sold</div>
                        <div class="value">
                            <asp:Label ID="lblTotalUnitsSold" runat="server"></asp:Label>
                        </div>
                    </article>

                    <article class="report-card">
                        <div class="label">Different Products Sold</div>
                        <div class="value">
                            <asp:Label ID="lblDifferentProductsSold" runat="server"></asp:Label>
                        </div>
                    </article>

                    <article class="report-card">
                        <div class="label">Products On Hand</div>
                        <div class="value">
                            <asp:Label ID="lblProductsOnHand" runat="server"></asp:Label>
                        </div>
                    </article>

                    <article class="report-card">
                        <div class="label">Low Stock Variants</div>
                        <div class="value">
                            <asp:Label ID="lblLowStockVariants" runat="server"></asp:Label>
                        </div>
                    </article>

                </div>

                <div class="chart-grid">

                    <article class="chart-card">
                        <h3>Product Sales</h3>
                        <p>Units sold for each smartphone model.</p>

                    <div style="margin-bottom: 18px;">

                        <label for="ddlProductFilter">
                            Filter by product:
                        </label>

                        <asp:DropDownList
                            ID="ddlProductFilter"
                            runat="server"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlProductFilter_SelectedIndexChanged">

                            <asp:ListItem
                                Text="All Products"
                                Value="All">
                            </asp:ListItem>

                        </asp:DropDownList>

                    </div>

                        <div class="chart-container">
                            <canvas id="productSalesChart"></canvas>
                        </div>
                    </article>

                    <article class="chart-card">
                        <h3>User Registrations</h3>
                        <p>PhoneFit accounts registered on each date.</p>

                        <div style="margin-bottom: 18px;">

                        <label for="ddlRegistrationFilter">
                            Filter by registration date:
                        </label>

                        <asp:DropDownList
                            ID="ddlRegistrationFilter"
                            runat="server"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlRegistrationFilter_SelectedIndexChanged">

                            <asp:ListItem
                                Text="All Dates"
                                Value="All">
                            </asp:ListItem>

                        </asp:DropDownList>

                    </div>

                        <div class="chart-container">
                            <canvas id="registrationChart"></canvas>
                        </div>
                    </article>

                </div>

                <div class="report-section">
                    <h2>Product Sales</h2>
                    <p>Units sold for each smartphone model that has appeared in a completed order.</p>

                    <asp:Panel ID="pnlNoProductSales" runat="server" Visible="false">
                        <div style="padding:30px; text-align:center; background:var(--paper); border:1px solid var(--rule); border-radius:var(--r);">
                            No product sales have been recorded yet.
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlProductSales" runat="server" Visible="false">
                        <div class="report-table-wrapper">
                            <table class="report-table">
                                <thead>
                                    <tr>
                                        <th>Product</th>
                                        <th>Units Sold</th>
                                    </tr>
                                </thead>

                                <tbody>
                                    <asp:Repeater ID="rptProductSales" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("ProductName") %></td>
                                                <td>
                                                    <span class="report-number">
                                                        <%# Eval("UnitsSold") %>
                                                    </span>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </asp:Panel>
                </div>


                <div class="report-section">
                    <h2>Registered Users Per Day</h2>
                    <p>Number of PhoneFit user accounts created on each registration date.</p>

                    <asp:Panel ID="pnlNoRegistrations" runat="server" Visible="false">
                        <div style="padding:30px; text-align:center; background:var(--paper); border:1px solid var(--rule); border-radius:var(--r);">
                            No user registration information is available.
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlRegistrations" runat="server" Visible="false">
                        <div class="report-table-wrapper">
                            <table class="report-table">
                                <thead>
                                    <tr>
                                        <th>Registration Date</th>
                                        <th>Users Registered</th>
                                    </tr>
                                </thead>

                                <tbody>
                                    <asp:Repeater ID="rptRegistrations" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Eval("RegistrationDate", "{0:dd MMM yyyy}") %>
                                                </td>
                                                <td>
                                                    <span class="report-number">
                                                        <%# Eval("UsersRegistered") %>
                                                    </span>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </section>
    </main>

    <script src="assets/js/chart.umd.min.js"></script>

    <script>
    document.addEventListener("DOMContentLoaded", function () {

        var productLabelsValue =
            document.getElementById('<%= hfProductLabels.ClientID %>').value;

        var productValuesValue =
            document.getElementById('<%= hfProductValues.ClientID %>').value;

        var registrationLabelsValue =
            document.getElementById('<%= hfRegistrationLabels.ClientID %>').value;

        var registrationValuesValue =
            document.getElementById('<%= hfRegistrationValues.ClientID %>').value;


        if (productLabelsValue !== "" && productValuesValue !== "") {

            var productLabels = productLabelsValue.split("|");
            var productValues = productValuesValue
                .split("|")
                .map(Number);

            var productCanvas =
                document.getElementById("productSalesChart");

            new Chart(productCanvas, {
                type: "bar",

                data: {
                    labels: productLabels,

                    datasets: [{
                        label: "Units Sold",
                        data: productValues,
                        backgroundColor: "#4F46E5"
                    }]
                },

                options: {
                    responsive: true,
                    maintainAspectRatio: false,

                    scales: {
                        y: {
                            beginAtZero: true,
                            ticks: {
                                precision: 0
                            }
                        }
                    }
                }
            });
        }


        if (registrationLabelsValue !== "" && registrationValuesValue !== "") {

            var registrationLabels =
                registrationLabelsValue.split("|");

            var registrationValues =
                registrationValuesValue
                    .split("|")
                    .map(Number);

            var registrationCanvas =
                document.getElementById("registrationChart");

            new Chart(registrationCanvas, {
                type: "bar",

                data: {
                    labels: registrationLabels,

                    datasets: [{
                        label: "Users Registered",
                        data: registrationValues,
                        backgroundColor: "#0F9F6E"
                    }]
                },

                options: {
                    responsive: true,
                    maintainAspectRatio: false,

                    scales: {
                        y: {
                            beginAtZero: true,
                            ticks: {
                                precision: 0
                            }
                        }
                    }
                }
            });
        }

    });
</script>
</asp:Content>
