<%@ Page Title="Manager Dashboard" Language="C#" MasterPageFile="~/Phonefit.Master" AutoEventWireup="true" CodeBehind="Manager.aspx.cs" Inherits="PhoneFit.Manager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .manager-grid {
            display: grid;
            grid-template-columns: repeat(2, 1fr);
            gap: 20px;
        }

        .manager-card {
            display: flex;
            flex-direction: column;
            justify-content: space-between;
            min-height: 210px;
            padding: 24px;
            background: var(--paper);
            border: 1px solid var(--rule);
            border-radius: var(--r);
        }

        .manager-card h2 {
            margin-bottom: 10px;
            font-size: 21px;
        }

        .manager-card p {
            margin-bottom: 24px;
            color: var(--fg-mute);
            line-height: 1.6;
        }

        .manager-card .card-label {
            margin-bottom: 10px;
            font-family: var(--ff-mono);
            font-size: 11px;
            text-transform: uppercase;
            letter-spacing: 0.07em;
            color: var(--indigo);
        }

        .manager-card .btn {
            align-self: flex-start;
        }

        @media (max-width: 760px) {
            .manager-grid {
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
                    <span>Manager Dashboard</span>
                </div>

                <h1>Manager Dashboard</h1>
                <p>
                    Manage the PhoneFit catalogue, brands, customer transactions,
                    registered users and business reports.
                </p>
            </div>
        </section>

        <section class="section">
            <div class="container">

                <div class="manager-grid">

                    <article class="manager-card">
                        <div>
                            <div class="card-label">Catalogue</div>
                            <h2>Product Management</h2>
                            <p>
                                View, add, edit and activate or deactivate smartphones.
                                Manage each phone's specifications and variants.
                            </p>
                        </div>

                        <a href="ManagerProducts.aspx" class="btn btn--indigo">
                            Manage Products
                        </a>
                    </article>

                    <article class="manager-card">
                        <div>
                            <div class="card-label">Catalogue</div>
                            <h2>Brand Management</h2>
                            <p>
                                View, add and edit smartphone brands. Activate or deactivate brands.
                            </p>
                        </div>

                        <a href="ManagerBrands.aspx" class="btn btn--indigo">
                            Manage Brands
                        </a>
                    </article>

                    <article class="manager-card">
                        <div>
                            <div class="card-label">Transactions</div>
                            <h2>Orders & Transactions</h2>
                            <p>
                                View customer orders, transaction totals and complete
                                purchase details.
                            </p>
                        </div>

                        <a href="ManagerOrders.aspx" class="btn btn--indigo">
                            View Orders
                        </a>
                    </article>

                    <article class="manager-card">
                        <div>
                            <div class="card-label">Accounts</div>
                            <h2>User Management</h2>
                            <p>
                                View registered PhoneFit users, assigned account roles,
                                account status and registration information.
                            </p>
                        </div>

                        <a href="ManagerUsers.aspx" class="btn btn--indigo">
                            View Users
                        </a>
                    </article>

                    <article class="manager-card">
                        <div>
                            <div class="card-label">Business</div>
                            <h2>Reports</h2>
                            <p>
                                View PhoneFit sales, stock, customer and transaction
                                reporting information.
                            </p>
                        </div>

                        <a href="ManagerReports.aspx" class="btn btn--indigo">
                            View Reports
                        </a>
                    </article>

                </div>

            </div>
        </section>

    </main>

</asp:Content>
