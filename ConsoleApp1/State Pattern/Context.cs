﻿using ShoppingCart.Models;
using ShoppingCart.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCart.Implementations;

namespace StatePat
{
    public class Context
    {
        public Cart cart = new Cart();
        public AddAb Add { get; set; }
        public DiscardAb Discard { get; set; }
        public PaymentAb Payment { get; set; }
        public void AddRequest(char c)

        {
            string result;
            switch (char.ToLower(c))
            {
                case 'm':
                    result = Add.AddMilk(this, cart);

                    break;
                case 'b':
                    result = Add.AddBread(this, cart);

                    break;
                case 'e':
                    result = Add.AddEggs(this, cart);

                    break;
                case 'o':
                    result = Add.AddOranges(this, cart);

                    break;
                case 'p':
                    result = Add.AddPepsi(this, cart);

                    break;

                case 'f': result = "";
                    break;
                default:
                    result = "\nTry again!";
                    break;
            }
            Console.WriteLine(result);
        }
        public void DiscardRequest(char c)

        {
            string result;
            switch (char.ToLower(c))
            {
                case 'm':
                    result = Discard.DiscardMilk(this, cart);

                    break;
                case 'b':
                    result = Discard.DiscardBread(this, cart);

                    break;
                case 'e':
                    result = Discard.DiscardEggs(this, cart);

                    break;
                case 'o':
                    result = Discard.DiscardOranges(this, cart);

                    break;
                case 'p':
                    result = Discard.DiscardPepsi(this, cart);
                    break;

                case 'f':
                    result = "";
                    break;

                default:
                    result = "\nTry again!";
                    break;
            }
            Console.WriteLine(result);
        }
        public void PaymentGatewayRequest(char c)

        {
            string result;
            switch (char.ToLower(c))
            {
                case 'c':
                    result = Payment.Card(this);
                    break;

                case 'n':
                    result = Payment.NetBanking(this);
                    break;
                                      
                case 'f':
                    result = "";
                    break;

                default:
                    result = "\nTry again!";
                    break;
            }
            Console.WriteLine(result);
        }

        public void PaymentMethodRequestCard(char c)
        {
            string result;
            switch (char.ToLower(c))
            {
                case 'c':
                    result = Payment.CardCITI(this);

                    break;
                case 'i':
                    result = Payment.CardIDBI(this);
                    break;


                case 'f':
                    result = "";
                    break;

                default:
                    result = "\nTry again!";
                    break;
            }
            Console.WriteLine(result);
        }
        public void PaymentMethodRequestNet(char c)
        {
            string result;
            switch (char.ToLower(c))
            {
                case 'c':
                    result = Payment.NetCITI(this);

                    break;
                case 'i':
                    result = Payment.NetIDBI(this);
                    break;


                case 'f':
                    result = "";
                    break;

                default:
                    result = "\nTry again!";
                    break;
            }
            Console.WriteLine(result);
        }
        public void PrintCart()
        {
            foreach (var item in cart._Cart)
            {
                Console.WriteLine(item.Name);
            }
        }
    }
}
