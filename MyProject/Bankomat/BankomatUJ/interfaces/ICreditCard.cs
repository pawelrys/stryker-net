﻿namespace BankomatUJ.interfaces;

/**
 * UWAGA:
 * 1. Nie wolno tworzyć publicznych pol jedynie można używac metod z interface
 * 2. Nie wolno dopisywac włsnych metod
 * 3. Nie wolno modyfikować interface
 * 4. Nie wolno zmieniać wersji javy ani junita.
 */
public interface ICreditCard
{
    /**
     * Metoda tworzy kartę.
     * @param newPin Nowy PIN
     * @param newPinConfirm Nowy PIN - wartość weryfikująca: newPin oraz newPinConfirm muszą być sobie równe.
     * @return Jeżeli uda się zmienić kod PIN, zwraca true. W przeciwnym razie zwraca false
     */
    void Init(string newPin, string newPinConfirm);


    /**
     * Metoda zmienia PIN karty na inny i zwraca true w przypadku poprawnej zmiany kodu PIN oraz false w przypadku nieudanej zmiany.
     * @param oldPin Stary PIN
     * @param newPin Nowy PIN
     * @param newPinConfirm Nowy PIN - wartość weryfikująca: newPin oraz newPinConfirm muszą być sobie równe.
     * @return Jeżeli uda się zmienić kod PIN, zwraca true. W przeciwnym razie zwraca false
     */
    bool ChangePin(string oldPin, string newPin, string newPinConfirm);


    /**
     * Metoda sprawdza, czy PIN danej karty jest poprawny
     * @param pin Kod PIN zapisany jako łańcuch znakowy (string)
     * @return Jeżeli kod PIN jest poprawny zwraca true, w przeciwnym wypadku zwraca false.
     */
    bool IsPinValid(string pin);


    /**
     * Metoda stowarzysza z daną kartą określone konto. Każda karta może być stowarzyszona z co najwyżej jednym kontem.
     * @param account Obiekt konta które będzie dodane do karty.
     */
    void AddAccount(IAccount? account);


    /**
     * Metoda zwraca stowarzyszone z daną kartą określone konto. Każda karta może być stowarzyszona z co najwyżej jednym kontem.
     * @return Zwraca obiekt typu IAccount
     */
    IAccount GetAccount();


    /**
     * Metoda realizuje wpłatę pieniędzy na konto stowarzyszone z tą kartą
     * @param amount Kwota jaką chcemy wpłacić
     * @return Zwraca true lub false w zależności od tego,
     * czy operacja się uda (może się nie udać jeśli na przykład obiekt konta dla danej karty jest = null).
     */
    bool DepositFunds(double amount);


    /**
     * Metoda realizuje wypłatę określonej kwoty z konta stowarzyszonego z daną kartą.
     * @param amount Kwota jaką chcemy wypłacić
     * @return Zwraca true lub false w zależności od tego, czy operacja się uda.
     */
    bool WithdrawFunds(double amount);
}