﻿using ShoppingCart.Abstractions;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Implementations
{
    public class Cart : BaseCart, ICart
    {
        public override List<Product> _Cart { get; set; } = new List<Product>();

        //discards item from cart
        public string DiscardItemFromCart(int id)
        {
            var rawProduct = this._Cart.FirstOrDefault(i => i.ProductID == id);
            this._Cart.Remove(rawProduct);
            if (rawProduct == null)
            {
                return "Failed to remove product from cart";
            }
            return "Product has been removed successfully";
        }
        //checks item , pretty much always true if product isnt null
        public bool CheckItemAvailability(string product)
        {
            if (product != null)
            {
                return true;
            }
            return false;

        }
        //sums product price into a cartprice
        public double GetCartPrice(int cartID)
        {

            var ProductsCost = this._Cart.Sum(i => i.Cost);
            Console.WriteLine("\t  Total price:" + ProductsCost);
            return ProductsCost;

        }
        //applies cart price based on user input for state
        public void ApplyTax(int cartID, double taxPercent)
        {
            double cartPrice = this._Cart.Sum(i => i.Cost);
            double taxTotal = cartPrice + cartPrice / taxPercent;
            Console.WriteLine("\t  Total money with tax:" + taxTotal);
            if (_Cart == null)
            {
                Console.WriteLine("Something bad happend");
            }

        }
        //gets item details
        public string GetItemDetails(int id)
        {
            var rawProduct = this._Cart.FirstOrDefault(i => i.ProductID == id);
            if (rawProduct == null)
            {
                return "Product not found";
            }
            return $"Product: {rawProduct.Name} - {rawProduct.Cost}";
        }
        //locks item
        public bool LockItemInStock(int productID)
        {
            if (productID != 0)
            {
                Console.WriteLine("Locking item");
                return true;
            }
            return false;
        }
        //adds item to cart
        public string AddItemToCart(Product product)
        {
            this._Cart.Add(product);
            if (product == null)
            {
                return "Failed to add product to cart";
            }
            return "Product has been succesfully added";
        }
        //FACADE--------
        public int CheckProduct(int productID)
        {
            Console.WriteLine("Checking product...");
            int cartID = 0;
            //Step 1 : GetItem
            string product = GetItemDetails(productID);
            //Step 2 : Check Availability
            if (product != "Product not found")
            {
                if (CheckItemAvailability(product))
                {
                    //Step 3 : Lock Item in the Stock
                    LockItemInStock(productID);
                }
            }
            Console.WriteLine("Check has ended");
            return cartID;
        }
        public int PlaceOrder(int cartID, int userID)
        {
            Console.WriteLine("Start PlaceOrderDetails");
            int orderID = -1;
            IWallet wallet = new Wallet();
            ITax tax = new Tax();
            IAddress address = new AddressDetails();
            IOrder order = new Order();
            //Step 1 : Get Tax percentage by State            
            string choice = "";
            char state = ' ';
            while (choice.Length != 1)
            {
                Console.WriteLine("Please select a state, options: a, b, c, d, note: default tax is 'b' if no valid options are selected!");
                choice = Console.ReadLine();
                if (choice == "" || choice.Length > 1)
                {
                    Console.WriteLine("\nInvalid input!");
                }
                else if (choice == "a" || choice == "b" || choice == "c" || choice == "d")
                {
                    state = choice[0];
                }
                else
                { choice = ""; }
            }
            double stateTax = tax.GetTaxByState(state);
            //Step 2 : Get user Wallet balance
            double userWalletBalance = wallet.GetUserBalance(userID);
            //Step 3 : Get the cart items price
            double cartPrice = GetCartPrice(cartID);
            //Step 4 : Apply Tax on the Cart Items
            ApplyTax(cartID, stateTax);
            //Step 5 : Compare the balance and price
            if (userWalletBalance > cartPrice)
            {
                //Step 6 : Get user Address and set to cart
                Address userAddress = address.GetAddressDetails(userID);
                //Step 7 : Place the order
                orderID = order.PlaceOrderDetails(cartID, userAddress.AddressID);
            }
            else
            {
                Console.WriteLine("Not enough money!");
            }
            Console.WriteLine("End PlaceOrderDetails");
            return orderID;
        }
    }
}

