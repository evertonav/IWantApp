# Projeto para aprimorar habilidades

Esse projeto foi baseado no curso https://www.udemy.com/course/net-6-web-api-do-zero-ao-avancado/.

# Ideia do projeto

É um aplicativo de pedidos, onde podemos fazer um pagamento, um pagamento bem simples onde apenas informamos o valor que pagou. Esse projeto tem autenticação, onde apenas os administradores podem cadastrar produtos e a sua categoria. Contando também com um relatório de produtos mais vendidos.

# Autenticação

A API utiliza OAuth2 como forma de autenticação/autorização.

## Solicitar token Acesso

Para solicitar o token de acesso você deve colocar [/token] utilizando o método `POST`. Uma dica, antes faça a criação de um cliente pelo [/Clientes] e utilize-o para buscar o token.

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

## Empregados [/empregados]

### Buscar todos empregados [GET /empregados{?pagina,linhas}]

+ Parameters
    + pagina (int, obrigatório) - Definir qual página deseja visualizar.
    + linhas (int, obrigatório) - Definir quantas linhas deseja visualizar.

+ Request (application/json)

    + Headers

            Authorization: Bearer [token]

+ Response 200 (application/json)

          [
              { 
                    "email": "evertonricardo1@gmail.com",
                    "nome": "Everton"
              },
              {
                    "email": "julia@gmail.com",
                    "nome": "julia"
              }               
          ]

+ Response 400 (application/json)

         {
             "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
             "title": "One or more validation errors occurred.",
             "status": 400,
             "errors": {
                 "Error": [
                     "Você precisa preencher o parâmetro 'pagina'!",
                     "Você precisa preencher o parâmetro 'linhas'!"
                 ]
             }
         }
         
         
### Novo Empregado [POST]  

+ Atributos (Objeto)
    + Email (string, obrigatório) - E-mail do empregado.
    + Senha (int, obrigatório) - Senha que o empregado irá usar para acessar os recursos.            
    + Nome (string, opcional) - Nome do empregado.
    + CodigoEmpregado (string, opcional) - Código que o empregado irá utilizar.
    
+ Request (application/json)

    + Headers

            Authorization: Bearer [token]    

    + Body
    
              { 
                    "Email": "testaralteracao1212@gmail.com",
                    "Senha": "123456",
                    "Nome": "",
                    "CodigoEmpregado": ""
              }    
    
+ Response 201 (application/json)

    + Body
            
            "02a152d7-96a6-4e1a-a8d1-388c1be5db63"
    
## Clientes [/clientes]

### Buscar Cliente [GET /clientes]

Com este recurso podemos resgatar os dados do usuário utilizado no token utilizado.

+ Request (application/json)

    + Headers

            Authorization: Bearer [token]  

+ Response 200 (application/json)

          {
               "id": "22c42772-0e0e-47c8-9b4e-055bf36764ee",
               "nome": "Everton"
          }


### Novo Cliente [POST]  

+ Atributos (Objeto)
    + Email (string, obrigatório) - E-mail do empregado.
    + Senha (int, obrigatório) - Senha que o empregado irá usar para acessar os recursos.            
    + Nome (string, opcional) - Nome do empregado.
    + CPF (string, opcional) - CPF do cliente.
    
+ Request (application/json)    

    + Body
    
              { 
                    "Email": "Testar@gmail.com",
                    "Senha": "123456",
                    "Nome": "Teste",
                    "CPF": "12345678912"
              }    
    
+ Response 201 (application/json)

     + Body
            
            "08c9d89c-6e5d-4ff5-92aa-c445e71402dc"

## Categorias [/categorias]

### Buscar todas categorias [GET /categorias]

+ Request (application/json)

    + Headers

            Authorization: Bearer [token]  

+ Response 200 (application/json)

          [
                {
                    "id": "9685108c-6b20-4ea4-ae3f-e4159dfcdb3a",
                    "nome": "Teste",
                    "ativo": true
                }
          ]
          
### Nova categoria [POST]  

+ Atributos (Objeto)
    + Nome (string, obrigatório) - Nome da categoria.
    + Ativo (boolean, opcional) - Se a categoria deve estar ativa ou não.                
    
+ Request (application/json)    

    + Body
    
              {
	            "Nome": "Teste",
	            "Ativo": true
              }           
    
+ Response 201 (application/json)

     + Body
            
            "04a8fea9-9e49-49aa-8350-eb4d2cd84462"          
            
### Editar categoria [PUT /categorias/{id}]            

+ Parameters
    + id (Guid, obrigatório) - Id da categoria.    

+ Request (application/json)

    + Headers

            Authorization: Bearer [token]

    + Body
    
              {
                        "Ativo": true,
                        "Nome": "Teste"
              }   

+ Response 200 (application/json)

## Produtos [/produtos]

### Buscar todos produtos [GET /produtos]

Este recurso tem uma política de authorização onde apenas quem é empregado pode visualizar, isto é, ao gerar o TOKEN utilize um usuário que é um empregado.

+ Request (application/json)

    + Headers

            Authorization: Bearer [token] 
            
+ Response 200 (application/json)

            [
                {
                    "id": "031c8030-81a7-4049-88b8-4d4f86c3e357",
                    "nome": "Extrato de Tomate",
                    "categoriaNome": "Extrato",
                    "descricao": "Extrato de Tomate",
                    "temEstoque": true,
                    "preco": 1.60,
                    "ativo": true
                }            
            ]                
            
### Buscar um produto especiífico  [GET /produtos/{Id}]          

Este recurso tem uma política de authorização onde apenas quem é empregado pode visualizar, isto é, ao gerar o TOKEN utilize um usuário que é um empregado.

+ Atributos (Objeto)
    + Id (Guid, obrigatório) - Id do produto.

+ Request (application/json)

    + Headers

            Authorization: Bearer [token] 
            
+ Response 200 (application/json)

            {
                   "id": "eaf3a8ad-e2dc-40ae-9356-fecac3d1f05b",
                   "nome": "Laranja",
                   "categoriaNome": "TesteServico",
                   "descricao": "Laranja",
                   "temEstoque": true,
                   "preco": 3.00,
                   "ativo": true
            } 
            
### Vitrine de produtos [GET /produtos/vitrine{?pagina,linhas,ordenarPor}]

Este recurso tem o objetivo de listar os produtos que tem estoque, e onde a categoria encontra-se ativa.

+ Atributos (Objeto)
    + pagina (int, obrigatório) - A página que deseja visualizar.
    + linhas (int, obrigatório) - Quantidade de linhas que deseja visualizar.
    + ordenarPor (string, opcional) - Que ordenação deseja utilizar, tem duas opções: "Nome" e "Preco". O default é Nome.    

+ Response 200 (application/json)

            [
                {
                    "id": "031c8030-81a7-4049-88b8-4d4f86c3e357",
                    "nome": "Extrato de Tomate",
                    "categoriaNome": "Extrato",
                    "descricao": "Extrato de Tomate",
                    "temEstoque": true,
                    "preco": 1.60,
                    "ativo": true
                }
            ]
            
### Novo produto [POST /produtos]            

+ Atributos (Objeto)
    + Nome (string, obrigatório) - Nome do produto.
    + CategoriaId (Guid, obrigatório) - Id de uma categória existente.                
    + Descricao (string, obrigatório) - Descrição do produto.
    + TemEstoque (boolean, opcional) - Define se o produto tem estoque ou não.
    + Preço (decimal, obrigatório) - O preço do produto.
    + Ativo (boolean, opcional) - Define se o produto está ativo.
    
+ Request (application/json)   

    + Headers

            Authorization: Bearer [token] 

    + Body
    
            {                   
                   "Nome": "Laranja",
                   "CategoriaId": "a04be7b1-b620-4d3c-a866-acd20718efde",
                   "Descricao": "Laranja",
                   "TemEstoque": true,
                   "Preco": 3.00,
                   "Ativo": true
            }            
    
+ Response 201 (application/json)

     + Body
            
            "d830f75a-a3a3-411a-9300-32d3f7f02d99"   
	    
### Relatório produtos mais vendidos [POST /produtos/relatorio/maisVendidos{?pagina, linhas}]     

+ Atributos (Objeto)
    + pagina (int, obrigatório) - A página que deseja visualizar.
    + linhas (int, obrigatório) - Quantidade de linhas que deseja visualizar.
    
+ Request (application/json)   

    + Headers

            Authorization: Bearer [token]        
    
+ Response 200 (application/json)

     + Body

            [
                {
                    "id": "031c8030-81a7-4049-88b8-4d4f86c3e357",
                    "nome": "Extrato de Tomate",
                    "quantidade": 1
                }
            ]                       

## Pedidos [/pedidos]

### Novo Pedido [POST /pedidos]

Neste recurso tem uma regra específica, onde apenas os clientes conseguem visualizar os pedidos, isto é, apenas os token que são referente a clientes.

+ Atributos (Objeto)
    + ProdutosIds (Array[Guid], obrigatório) - São uma lista de Ids de produtos que foram cadastrados no sistema.
    + EnderecoEntrega (string, obrigatório) - O endereço onde vão ser entregues os produtos listados.

+ Request (application/json)   

    + Headers

            Authorization: Bearer [token]
	    
    + Body
    
            {                   
                   "ProdutosIds": ["eaf3a8ad-e2dc-40ae-9356-fecac3d1f05b", "031c8030-81a7-4049-88b8-4d4f86c3e357"],
                   "EnderecoEntrega": "Rua 1, teste"
            }

+ Response 201 (application/json)

     + Body
	
	f7ec6162-b3b2-4b6f-8b1b-44f91f27db6e
	    	    
### Buscar um Pedido [POST /pedidos/{Id}]

+ Atributos (Objeto)
    + Id (Guid, obrigatório) - Id do pedido.

+ Request (application/json)   

    + Headers

            Authorization: Bearer [token]

+ Response 200 (application/json)

     + Body
	
		{
		    "id": "031c8030-81a7-4049-88b8-4d4f86c3e357",
		    "nome": "Extrato de Tomate"
		},
		{
		    "id": "eaf3a8ad-e2dc-40ae-9356-fecac3d1f05b",
		    "nome": "Laranja"
		}
	    ],
	    "enderecoEntrega": "cefer 2, na baixada"
       }
	    
    + Body
    
            {                   
                   "id": "f7ec6162-b3b2-4b6f-8b1b-44f91f27db6e",
                   "emailCliente": "TesteClientes1",
		   	 "produtos": [
					{
					    "id": "031c8030-81a7-4049-88b8-4d4f86c3e357",
					    "nome": "Extrato de Tomate"
					},
					{
					    "id": "eaf3a8ad-e2dc-40ae-9356-fecac3d1f05b",
					    "nome": "Laranja"
					}
	    	  	 ],
		   	"enderecoEntrega": "tESTE"
            }	   
