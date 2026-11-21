# Quiz Battle

Det här är vårt grupparbete i C#. Vi har gjort ett frågespel som körs i konsolen där 2–4 personer spelar vid samma dator.

## Starta spelet

Projektet använder .NET 10.

Klona repot och öppna projektet i Visual Studio. Bygg projektet och tryck sedan på Ctrl + F5 för att starta.

Det går också att starta genom terminalen:

```bash
git clone https://github.com/rebeccasjobolgmailcom/Quiz-battle.git
cd Quiz-battle/Quiz-Battle
dotnet build
dotnet run
```

## Hur man spelar

I menyn kan man välja att spela, visa topplistan eller avsluta.

När man startar anger man antal spelare, spelarnas namn och hur många rundor man vill spela. Man kan välja mellan 1 och 10 rundor.

Spelarna får sedan välja kategori eller slumpa. Det finns flervalsfrågor och sant eller falskt-frågor. Efter varje svar visas om det var rätt och hur många poäng spelaren har.

En fråga ska bara komma en gång under samma spel. Om frågorna tar slut avslutas spelet och resultatet visas.

## Poäng

- Lätt fråga ger 10 poäng.
- Medelsvår fråga ger 20 poäng.
- Svår fråga ger 30 poäng.
- Fel svar ger 0 poäng.

Svarar man rätt flera gånger i rad får man bonus. Den börjar på 5 extra poäng vid andra rätta svaret och ökar upp till 20. Vid fel svar börjar serien om.

Den med flest poäng vinner. Vid lika poäng jämförs antal rätta svar. Är det också lika blir det delad seger.

## Filer och klasser

Frågorna finns i `data/questions.json` och resultaten sparas i en separat JSON-fil för topplistan.

`Player` håller reda på varje spelares poäng och svar. `QuizGame` håller ihop rundorna och räknar ut vilka som vinner. `QuestionBank` läser in och hämtar frågorna.

Vi har använt arv för de två frågetyperna och ett interface för hanteringen av topplistan.

## Fördelning av arbetet

- Rebecca jobbade med frågorna och frågebanken.
- Ludwig jobbade med Player och QuizGame.
- Jakob jobbade med menyn, inmatningen och topplistan.

Vi började med att dela upp uppgiften och göra ett UML-diagram. Under arbetet använde vi egna brancher i GitHub och hjälptes åt med att koppla ihop delarna.
