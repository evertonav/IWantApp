# Projeto para aprimorar habilidades

Esse projeto foi baseado no curso https://www.udemy.com/course/net-6-web-api-do-zero-ao-avancado/.

# Ideia do projeto

É um aplicativo de pedidos, onde podemos fazer um pagamento, um pagamento bem simples onde apenas informamos o valor que pagou. Esse projeto tem autenticação, onde apenas os administradores podem cadastrar produtos e a sua categoria. Contando também com um relatório de produtos mais vendidos.

# Autenticação

A API utiliza OAuth2 como forma de autenticação/autorização.

## Solicitar token Acesso

Para solicitar o token de acesso você deve colocar [/token] utilizando o método `POST`.

#### Dados para envio no POST
| Parâmetro | Descrição |
|---|---|
| `email` | Informar: Um e-mail cadastrado em clientes ou empregado .|
| `senha` | Senha utilizada no cadastro de e-mail. |


+ Request (application/json)

    + Body

            {
              "email": "testeAssincrono@gmail.com"
              "senha": "12345"
            }

+ Response 200 (application/json)

+ Body

            {
                "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InRlc3RlQXNzaW5jcm9ub0BnbWFpbC5jb20iLCJuYW1laWQiOiI2NWJlNzc0Yy1jYThjLTRmOWItYWM2YS1lZWRiYWYzNWQ2NDEiLCJDb2RpZ29FbXByZWdhZG8iOiIyIiwiTm9tZSI6Ikpvc8OpIiwiQ3JpYWRvUG9yIjoiOWU4YTMxZmMtNTk5Mi00OTU1LThmZjUtZWE5Zjk5MGQyMjlkIiwibmJmIjoxNjc0NjA2NDIyLCJleHAiOjE3MDYxNDI0MjIsImlhdCI6MTY3NDYwNjQyMiwiaXNzIjoiV2FudEFwcElzc3VlciIsImF1ZCI6IkF1ZGllbmNlIn0.enku9CI3d3oMNF9NIgIs08O6OuQMlbNNckZQJiBj8Lc"
            }

# Recursos


