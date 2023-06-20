﻿namespace BankomatUJ.interfaces;

/**
 * UWAGA:
 * 1. Nie wolno tworzyć publicznych pol jedynie można używac metod z interface
 * 2. Nie wolno dopisywac włsnych metod
 * 3. Nie wolno modyfikować interface
 * 4. Nie wolno zmieniać wersji javy ani junita.
 */
public interface IAtm
{
    /**
     * Metoda sprawdza poprawność kodu PIN. Jeśli jest on poprawny,
     * zwraca prawdę (co jest równoznaczne z uznaniem klienta za zalogowanego),
     * a w przeciwnym wypadku zwraca fałsz (co oznacza odmowę zalogowania klienta).
     * Karta kredytowa w tym kontekście jest obiektem klasy implementującej interfejs ICreditCard
     * Poprawny kod PIN musi składać się z 4 znaków, każdy z nich musi być cyfrą.
     * @param pin Kod pin w formie stringa
     * @param creditCard Obiekt karty kredytowej implementujący
    interface ICreditCard
     * @return true jeśli PIN jest poprawny, false jeśli błędny
     */
    bool CheckPinAndLogIn(ICreditCard? creditCard, string pin);


    /**
     * Metoda zwraca bieżącą wartość salda na koncie
     * Przykład: dla konta z saldem 362 PLN metoda zwróci wartość 362
     * @param account konto, dla którego ma zostać zwrócone saldo
     * @return Aktualne saldo konta jako wartość typu double.
     */
    double AccountStatus(IAccount? account);


    /**
     * Metoda zmienia PIN karty i zwraca true w przypadku poprawnej
     * zmiany kodu PIN oraz false w przypadku nieudanej zmiany. Zmiana jest
     * poprawna, jeśli nowy PIN jest poprawny.
     * @param card Obiekt karty kredytowej
     * @param oldPin Stary PIN
     * @param newPin Nowy PIN
     * @param newPinConfirm Nowy PIN - wartość weryfikująca: newPin oraz newPinConfirm muszą być sobie równe.
     * @return Jeżeli uda się zmienić pin zwraca true.
     */
    bool ChangePinCard(ICreditCard? card, string oldPin, string newPin, string newPinConfirm);


    /**
     * Metoda realizuje wpłatę pieniędzy na konto stowarzyszone z daną kartą
     * @param card Karta kredytowa stowarzyszona z pewnym kontem
     * @param amount Dodatnia kwota jaką chcemy wpłacić
     * @return Zwraca true lub false w zależności od tego, czyoperacja się uda (może się nie udać,
     * jeśli na przykład obiekt konta stowarzyszonego z kartą jest = null).
     */
    bool DepositFunds(ICreditCard? card, double amount);


    /**
     * Metoda realizuje wypłatę określonej kwoty z konta stowarzyszonego z określoną kartą.
     * @param card Karta kredytowa stowarzyszona z kontem
     * @param amount Dodatnia kwota jaką chcemy wypłacić
     * @return Zwraca true lub false w zależności od tego, czy
    operacja wypłaty się uda.
     */
    bool WithdrawFunds(ICreditCard? card, double amount);


    /**
     * Metoda realizuje przelew określonej kwoty (amount) z konta stowarzyszonego z daną kartą (card) na określone konto (accountRecipient)
     * Jeżeli operacja się uda metoda zwraca true, w przeciwnym wypadku zwraca false.
     * Operacja może się nie udać na przykład gdy mamy
     niewystarczającą ilość środków na koncie.
     * @param card Karta którą dokonujemy transfer
     * @param accountRecipient Konto odbiorcy
     * @param amount Dodatnia kwota, którą chcemy przelać
     * @return Zwraca true lub false w zależności od tego, czy
    operacja się uda.
     */
    bool Transfer(ICreditCard? card, IAccount? accountRecipient, double amount);
}