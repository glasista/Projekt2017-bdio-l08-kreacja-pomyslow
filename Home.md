# Role w Systemie kreacji pomysłów:
(wersja z uwzględnionymi „ostatecznymi” poprawkami z dn. 27.03.2017r.)
### Założenia pisanego przez nas programu są na stronie o nazwie "Specyfikacja"! To jest wersja pełna z wszystkimi funkcjonalnościami, włącznie z tymi których nie realizujemy.
## Administrator
  * potwierdza konta pracowników
  * dodaje/edytuje/usuwa kategorie pomysłów/problemów
  * dodaje/edytuje/usuwa poszczególne „kategorie” (kryteria) oceny:
    (w tym trzy domyślne dla wszystkich pomysłów, tzw. 3P: 
     - przydatność
     - poziom trudności
     - pomysłowość
    w ocenionych już pomysłach kategorie te się nie zmieniają)
  * przydziela/usuwa pracownika do kategorii pomysłów/problemów.
  * ustala stan projektu (pomysłu/problemu)
    - domyślny stan to oczekujący na przydzielony/odrzucony
  * przydziela pracownika do zgłoszonego projektu (pomysłu/problemu)
  * odrzuca zgłoszony projekt: (-stan odrzucony)
  * wyświetla i edytuje dane w bazie studentów/pracowników
  * usuwa konto wybranego studenta/pracownika z bazy studentów/pracowników
  * usuwa wybrany projekt z bazy pomysłów/problemów
  * przydziela użytkowników do oceny pomysłu 
  * sortuje (rosnąco/malejąco) po dacie/kategorii/stanie
  * wyszukuje po frazie w bazie projektów: problemów/pomysłów


## Student
  * tworzy konto w systemie: podaje: Imię, Nazwisko, Kierunek studiów, 
    e-mail studencki, hasło 
  * loguje się w systemie za pomocą e-maila i hasła
  * wyświetla publiczne opisy projektów z bazy problemów/pomysłów
  * wyświetla niepubliczne opisy projektów z bazy problemów/pomysłów,
    jeżeli je zgłosił (lub ma ocenić pomysł)
  * ocenia projekty - pomysły innych studentów, przydzielone mu do oceny
    przez administratora (publiczne i niepubliczne)
    - korzysta  z formularza oceny w dostępnych kategoriach
    - wybiera odpowiednią ilość gwiazdek (1-5)
  * zgłasza projekt (problem lub pomysł) przy pomocy formularza, w którym:
    - Wybiera typ pomysłu: problem/pomysł
    - Wybiera status: publiczny/niepubliczny
    - Kategorie: dla problemu/dla pomysłu
    - Określa tytuł: napis do 50 znaków
    - Dodaje opis: napis do 1000 znaków
    - Dodaje plik lub kilka plików powiązanych z pomysłem/problemem
  * wyszukuje po frazie w bazie projektów: problemów/pomysłów
  * sortuje (rosnąco/malejąco) po dacie/kategorii/stanie

  #### Na swoim koncie
  
  * dostaje powiadomienia o zmianach statusu swojego projektu
    - pomysłu: z oczekujący na przydzielony/odrzucony
      następnie z przydzielony na zrealizowany/niezrealizowany
    - problemu: z oczekujący na przydzielony/odrzucony 
      następnie z z przydzielony na rozwiązany/nierozwiązany
    - przydzieleniu do oceny projektu innego użytkownika
  * wyświetla ocenę własnego projektu:
    - średnią
    - szczegółową (wyróżnione poszczególne kryteria)
  * wyświetla zgłoszone przez siebie projekty: pomysły/problemy
  * wyświetla dokonane przez siebie oceny przydzielonych projektów, innych użytkowników:
    - średnią
    - szczegółową (wyróżnione poszczególne kryteria)
  * wyświetla średnią ocenę własnego projektu (pomysłu) dokonaną przez wyznaczonych użytkowników
    - średnią
    - szczegółową (wyróżnione poszczególne kryteria)
  * zmienia hasło w razie potrzeby


## Pracownik
  * tworzy konto w systemie: podaje: Imię, Nazwisko, katedrę/zakład, e-mail, hasło 
  * loguje się w systemie za pomocą e-maila i hasła.
  * wyświetla publiczne opisy projektów z bazy problemów/pomysłów
  * wyświetla niepubliczne opisy projektów z bazy problemów/pomysłów, 
    jeżeli jest do nich przydzielony (lub ma ocenić pomysł)
  * wyszukuje po frazie w bazie projektów: problemów/pomysłów
  * sortuje (rosnąco/malejąco) po dacie/kategorii/stanie

  #### Na swoim koncie

  * otrzymuje powiadomienia o:
    - przydzieleniu do realizacji projektu: problemu/pomysłu
    - przydzieleniu do danej kategorii: problemu/pomysłu
    - przydzieleniu do oceny pomysłu innego użytkownika
  * wyświetla dane kontaktowe użytkowników ze swoich projektów
  * wyświetla przydzielone do niego projekty
  * wyświetla kategorie projektów, do których został przydzielony
  * wyświetla zgłoszone przez siebie projekty: pomysły/problemy
  * wyświetla średnią ocenę własnego projektu
    pomysłu dokonaną przez wyznaczonych użytkowników
    - średnią
    - szczegółową (wyróżnione poszczególne kryteria)
  * wyświetla dokonane przez siebie oceny przydzielonych projektów innych użytkowników
    - średnią
    - szczegółową (wyróżnione poszczególne kryteria)
  * zmienia status przydzielonych projektów na:
    - Pomysł: zrealizowany/niezrealizowany
    - Problem: rozwiązany/nierozwiązany
  * zmienia hasło/e-mail w razie potrzeby


## Gość
  * wyświetla publiczne projekty z baz, stronę główną, o projekcie
  * zakłada konto w razie potrzeby, 
    w przeciwnym wypadku nie ma dostępu do pozostałych funkcjonalności
