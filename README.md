# Simple Api Disney
#### Servidor de prueba: **http://149.56.175.197/api/**

### Registrando Un Nuevo Usuario

Para comenzar a usar esta Api se debe deben registrar en el Endpoint:
```php
 curl -X POST  /api/auth/register
```

Se debe usar el método "Post" y se debe enviar el siguiente Json en la llamada a la Api:
```Json
{
    "Email":"",
    "Username":"",
    "Nombre":"",
    "Apellido":"",
    "Password":"",
    "Imagen":""
}
```

Esta llamada responderá con los datos del nuevo usuario más un token que se usara para poder consumir la Api.


### Recuperando Datos y Token

Si ya se esta registrado se pueden recuperar los datos y el Token haciendo una llamada al Endpoint:
```php
 curl -X POST  /api/auth/login
```
Cuerpo de la llamada debe tener los siguientes parametros:

```Json
{
    "Email":"",
    "Password":""
}
```


**Respuesta:**
```Json
{
    "id": "268a6ca4-cdb7-47bf-a32d-a05921271741",
    "email": "masterapi@lidioguedez.com",
    "username": "admmaster",
    "token": "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.   eyJlbWFpbCI6Im1hc3RlcmFwaUBsaWRpb2d1ZWRlei5jb20iLCJuYW1lIjoiQWRtaW4iLCJmYW1pbHlfbmFtZSI6Ik1hc3RlciIsInVzZXJuYW1lIjoiYWRtbWFzdGVyIiwibmJmIjoxNjYzMDk2MjUyLCJleHAiOjE2NjMxMTA2NTIsImlhdCI6MTY2MzA5NjI1MiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MjA1In0.59otX81PtxyyJeZLQwqplFU0-aWmlomXqV6JsONIT6RBb1ZuP1maNWCar97DJYX-NJAB9UCxfrOSDaOhQg0-rQ",
    "nombre": "Admin",
    "apellido": "Master",
    "imagen": "imagen.jpg",
    "admin": false
}
```

## Comenzando a Cargar Información en la Api
Para comenzar a usar la Api lo primero que se debe hacer es crear un nuevo Personaje

### Listar todos los Personajes
```php
 curl -X GET -H 'Authorization: Bearer $TOKEN'    /api/Characters
```

### Ver Detalle de cada Personaje
Indicando el Id del Personaje se puede obtener el detalle de cada personaje.
```php
 curl -X GET -H 'Authorization: Bearer $TOKEN'    /api/Characters/{id}
```
**Respuesta:**
```Json
{
    "personajeId": 1,
    "nombre": "Baronesa Von Hellman",
    "edad": 50,
    "peso": 65,
    "historia": "La baronesa Von Hellman es una conocida diseñadora de moda y propietaria de la empresa de alta costura House of Baroness. Su notoriedad la ha convertido en una figura pública y en una especie de modelo para la joven Estella. Durante una visita sorpresa a una de sus muchas tiendas en Londres, la Baronesa se fija en el salvaje escaparate. También es testigo de la detención de Estella y sus dos amigos Gaspar y Horacio. La baronesa se entera de que Estella es la responsable del escaparate y elogia su talento, diciéndole fríamente a la dueña de la tienda que es el mejor escaparate que tiene la tienda en años. Inmediatamente contrata a Estella como diseñadora en House of the Baroness.",
    "imagen": "https://static.wikia.nocookie.net/disney/images/9/9a/Cruella_-_Photography_-_Baroness_2.jpg/revision/latest/scale-to-width-down/1000?cb=20210601151044&path-prefix=es",
    "data": [
        {
            "pelicula": {
                "peliculaId": 4,
                "titulo": "101 dálmatas 2",
                "imagen": "",
                "fechaCreacion": "",
                "calificacion": 0,
                "generoId": 1
            }
        }
    ]
}
```


### Creando un Personaje
Se debe hacer una llamada al Endpoint siguiente con el método Post:

```php
 curl -X POST -H 'Authorization: Bearer $TOKEN'    /api/Characters
```

**Cuerpo de la petición:**
```Json
{
    "Nombre":"",
    "Edad":0,
    "Peso":0,
    "Historia":"",
    "Imagen":"",
    "PeliculasId": []
}
```

El Array **"PeliculasId"** se envia vacio.

**Respuesta:**
```Json
{
    "personajeId": 5,
    "nombre": "Pato Donald",
    "imagen": "https://static.wikia.nocookie.net/disney/images/9/9a/Cruella_-_Photography_-_Baroness_2.jpg/revision/latest/scale-to-width-down/1000?cb=20210601151044&path-prefix=es"
}
```




### Editando Personaje
Para editar un personaje se puede enviar una peticion Put al siguiente Endpoint, se debe indicar en la misma url el ID del Personaje.

```php
 curl -X PUT -H 'Authorization: Bearer $TOKEN' /api/Characters/{id}
```

Se debe incluir los siguientes datos minimos:
```Json
{
    "nombre": "Pato Donald",
    "edad": 50,
    "peso": 40,
    "imagen": "https://static.wikia.nocookie.net/disney/images/9/9a/Cruella_-_Photography_-_Baroness_2.jpg/revision/latest/scale-to-width-down/1000?cb=20210601151044&path-prefix=es",
    "historia": "Pato Donald (Donald Duck en inglés) es un personaje de Disney de la serie Mickey Mouse, caracterizado como un pato antropomórfico de color blanco y con el pico, las piernas y las patas anaranjadas. Generalmente viste una camisa de estilo marinero y un sombrero, sin pantalones, excepto cuando va a nadar.",
    "PeliculasId" : [
          1
    ]
}
```
**NOTA:** En **"PeliculasId"** se agregan o retiran los Ids de cada pelicula donde el personaje participa. Se pueden agregar o quitar

**Respuesta:**
```Json
{
    "personajeId": 5,
    "nombre": "Pato Donald",
    "edad": 50,
    "peso": 40,
    "historia": "Pato Donald (Donald Duck en inglés) es un personaje de Disney de la serie Mickey Mouse, caracterizado como un pato antropomórfico de color blanco y con el pico, las piernas y las patas anaranjadas. Generalmente viste una camisa de estilo marinero y un sombrero, sin pantalones, excepto cuando va a nadar.",
    "imagen": "https://static.wikia.nocookie.net/disney/images/9/9a/Cruella_-_Photography_-_Baroness_2.jpg/revision/latest/scale-to-width-down/1000?cb=20210601151044&path-prefix=es",
    "data": [
        {
            "pelicula": {
                "peliculaId": 3,
                "titulo": "Mickey, Donald, Goofy: Los tres mosqueteros",
                "imagen": "htp://imagen.com",
                "fechaCreacion": "2004",
                "calificacion": 0,
                "generoId": 1
            }
        }
    ]
}
```

### Eliminando Personaje
Se puede eliminar un personaje haciendo una peticion al siguiente endpoint:
```php
 curl -X DELETE -H 'Authorization: Bearer $TOKEN' /api/Characters/{id}
```
Devolvera un string indicando que fue eliminado: **"Personaje Eliminado"**


### Busquedas de Personajes
Se puede hacer busqueda por **nombre:**
```php
 curl -X GET -H 'Authorization: Bearer $TOKEN' /api/characters?name=nombre
```
Por **edad:**
```php
 curl -X GET -H 'Authorization: Bearer $TOKEN' /api/characters?age=edad
```
Por Id de **Pelicula:**
```php
 curl -X GET -H 'Authorization: Bearer $TOKEN' /api/characters?movies=idMovie
```


## Creación de Peliculas y Ordenamiento

### Listar todas las Peliculas
```php
 curl -X GET -H 'Authorization: Bearer $TOKEN' /api/movies
```
**Respuesta:**
 ```Json
 [
    {
        "peliculaId": 3,
        "titulo": "Mickey, Donald, Goofy: Los tres mosqueteros",
        "imagen": "htp://imagen.com",
        "fechaCreacion": "2004",
        "generoNombre": "Animada"
    },
    {
        "peliculaId": 4,
        "titulo": "101 dálmatas 2",
        "imagen": null,
        "fechaCreacion": null,
        "generoNombre": "Animada"
    }
]
 ```

 ### Ver detalle de una Pelicula
 Se envia el Id de la pelicula para consultar el detalle
```php
 curl -X GET -H 'Authorization: Bearer $TOKEN' /api/movies/{id}
```
**Respuesta:**
```Json
{
    "peliculaId": 3,
    "titulo": "Mickey, Donald, Goofy: Los tres mosqueteros",
    "imagen": "htp://imagen.com",
    "fechaCreacion": "2004",
    "calificacion": 0,
    "generoId": 1,
    "generoNombre": "Animada",
    "data": [
        {
            "personaje": {
                "personajeId": 3,
                "nombre": "Pato Donald",
                "imagen": "https://static.wikia.nocookie.net/disney/images/9/9a/Cruella_-_Photography_-_Baroness_2.jpg/revision/latest/scale-to-width-down/1000?cb=20210601151044&path-prefix=es"
            }
        }
    ]
}
```

### Creación de nueva Pelicula
Se debe hacer una solicitud al Endpoint  /movies

```php
 curl -X POST -H 'Authorization: Bearer $TOKEN' /api/movies
```

```Json
{

    "Titulo": "101 dálmatas",
    "GeneroId": 1,
    "Imagen": "htp://imagen.com",
    "FechaCreacion": "1961",
    "PersonajeID" : [
       1
    ]

}
```

**Respuesta:**

```Json
{
    "peliculaId": 5,
    "titulo": "101 dálmatas",
    "imagen": "htp://imagen.com",
    "fechaCreacion": "1961",
    "calificacion": 0,
    "generoId": 1,
    "generoNombre": "Animada",
    "data": [
        {
            "personaje": {
                "personajeId": 1,
                "nombre": "Baronesa Von Hellman",
                "imagen": "https://static.wikia.nocookie.net/disney/images/9/9a/Cruella_-_Photography_-_Baroness_2.jpg/revision/latest/scale-to-width-down/1000?cb=20210601151044&path-prefix=es"
            }
        }
    ]
}
```

### Edicion de Pelicula
Para editar una pelicula se debe hacer una solicitud de tipo PUT e indicar el ID de la pelicula.
```php
 curl -X PUT -H 'Authorization: Bearer $TOKEN' /api/movies/{id}
```
Estos son los campos minimos que se deben usar:
```Json
{
    "Titulo": "110 dálmatas",
    "GeneroId": 1,
    "PersonajeId": [
        1,4
    ]

}
```

**Respuesta:**

```Json
{
    "peliculaId": 4,
    "titulo": "110 dálmatas",
    "imagen": null,
    "fechaCreacion": null,
    "calificacion": 0,
    "generoId": 1,
    "generoNombre": "Animada",
    "data": [
        {
            "personaje": {
                "personajeId": 1,
                "nombre": "Baronesa Von Hellman",
                "imagen": "https://static.wikia.nocookie.net/disney/images/9/9a/Cruella_-_Photography_-_Baroness_2.jpg/revision/latest/scale-to-width-down/1000?cb=20210601151044&path-prefix=es"
            }
        },
        {
            "personaje": {
                "personajeId": 4,
                "nombre": "Cruella de Vil",
                "imagen": "https://static.wikia.nocookie.net/doblaje/images/b/bd/Cruella_De_Vil-0.png/revision/latest/scale-to-width-down/341?cb=20180716034935&path-prefix=es"
            }
        }
    ]
}
```

### Eliminación de Pelicula
La eliminacion de un registro se hae invocando el metodo Delete al enpoint /movie indicandole el id a eliminar.
```php
 curl -X DELETE -H 'Authorization: Bearer $TOKEN' /api/movies/{id}
```

Devolvera un string idicando si la pelicula fue borrada (**Pelicula Eliminada**) o sino un error.

### Ordenamientos y Busquedas

Se puede buscar por titulo de la pelicula.
```php
 curl -X GET -H 'Authorization: Bearer $TOKEN' /api/movies?name=nombre
```

Se puede filtrar por Genero de la Pelicula.
```php
 curl -X GET -H 'Authorization: Bearer $TOKEN' /api/movies?genre=idGenero
```

Se puede ordenar por facha de Creación
```php
 curl -X GET -H 'Authorization: Bearer $TOKEN' /api/movies?order=ASC | DESC
```








