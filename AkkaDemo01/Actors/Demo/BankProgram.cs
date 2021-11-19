using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AkkaDemo01.Actors.Demo
{
    public class BankProgram
    {
        private object syncLock = new object();
        private Account clientBankAccount;

        public BankProgram()
        {
            clientBankAccount = new Account();
        }

        public async Task Run()
        {
            try
            {
                var addToAccountTask = Task.Run(() =>
                {
                    Console.WriteLine("Tread Id {0}, Account balance before: {1}",
                        Thread.CurrentThread.ManagedThreadId, clientBankAccount.Balance);
                    lock (syncLock)
                    {
                        Console.WriteLine("Tread Id {0}, Adding 10 to balance",
                            Thread.CurrentThread.ManagedThreadId);

                        clientBankAccount.Balance += 10;

                        Console.WriteLine("Tread Id {0}, Account balance after: {1}",
                            Thread.CurrentThread.ManagedThreadId, clientBankAccount.Balance);
                    }
                });
                var subtractAccountTask = Task.Run(() =>
                {
                    Console.WriteLine("Tread Id {0}, Account balance before: {1}",
                        Thread.CurrentThread.ManagedThreadId, clientBankAccount.Balance);

                    lock (syncLock)
                    {
                        Console.WriteLine("Tread Id {0}, Subtracting 4 to balance",
                            Thread.CurrentThread.ManagedThreadId);

                        clientBankAccount.Balance -= 4;

                        Console.WriteLine("Tread Id {0}, Account balance after: {1}",
                            Thread.CurrentThread.ManagedThreadId, clientBankAccount.Balance);
                    }
                });

                await Task.WhenAll(addToAccountTask, subtractAccountTask);

                Console.WriteLine("Tread Id {0}, At the last Account balance: {1}",
                    Thread.CurrentThread.ManagedThreadId, clientBankAccount.Balance);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
