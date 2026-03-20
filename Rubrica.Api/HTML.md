L'HTML  è un markup language per creare le pagine web. Esso definisce la struttura e il contenuto di una pagina web utilizzando tag e attributi.

## Tag
Un tag è un elemento di base di HTML, che viene utilizzato per definire la struttura e il contenuto di una pagina eb. I tag sono racchiusi tra parentesi angolari < > e possono essere di apertura o di chiusura.

Esempio di tag apertura e chiusura

```html
<p> questo è un paragrafo </p>
```

I tag vanno messi in ordine opposto, tipo se abbiamo un paragrafo con un grassetto, il tag di chiusura del paragrafo va dopo il tag di chiusura del grassetto

```html
<p><b>paragrafo</b></p>
```

e non così:

```html
<p><b>paragrafo</p></b>
```

cioè l'ultimo tag aperto è il primo da chiudere.
Alcuni tag hanno un valore semantico, cioè indicano al browser ed ai motori di ricerca il significato del contenuto, ad esempio `<h1>` indica un titolo principale, mentre `<strong>` indica un testo importante.
Altri tag invece sono utilizzati principalmente per la formattazione del testo, come `<b>` per il grassetto.


## Attributi

Gli attributi sono utilizzati per fornire ulteriori informazioni sui tag. Gli attributi sono scritti all'interno del tag di apertura e sono composti da un nome e da un valore
Esempio di tag con attributo

```html
<p class="testo">Questo è un paragrafo</p>
```

- p è il nome del tag
- class è il nome dell'attributo
- testo è il valore dell'attributo

## Pagina HTML
La struttura di una pagia HTML è composta da diversi elementi, tra cui head e body.
- Head contiene informazioni sulla pagina, come il titolo e i link ai file CSS e Javascript.
- Body contiene il contenuto della pagina, come testo, immagini e altri elementi

Esempio di pagina base HTML:

```html
<!DOCTYPE html>
<html>
<head>
    <title>La mia pagina web</title>
</head>
<body>
    <h1>Benvenuti nella mia pagina</h1>
    <p>Questo è un paragrafo di esempio</p>
</body>
```

I commenti in HTML si scrivono così:
<!-- Questo è un commento html -->

## HEAD
Generalmente nll' head si mettono le informazioni riguardanti:

- Il titolo della pagina
- i link ai file CSS
- Le indicaioni riguardanti il viewport per la responsività
- Le indicazioni sulla localizzazione della pagina
- Le indicazioni sulla codifica dei caratteri della pagina

Quindi un esempio completo potrebbe essere:

```html
<head>
    <!-- Informazioni sulla pagina -->
    <title>La mia pagina web</title>
    <link rel="stylesheet" href="style.css">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="language" content="it">
    <meta charset="UTF-8">
</head>
```
