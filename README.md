# Consumindo API Arquivei para buscar as NFes

## Descrição:
Trata-se de uma aplicação do tipo Console que irá consumir a API disponibilizada pela Arquivei e salvar a chave de acesso e valor total das notas fiscais eletronicas em uma base de dados SQL Server.

## Como Utilizar:
1. Em [release](https://github.com/rrpatreze/arquivei/releases), fazer o download do arquivo *CarregarNotas.zip*;
2. Descompactar o arquivo em um diretório local;
3. Executar o arquivo *Receiver.exe*

## Resultado Esperado:
Ao executar, um prompt do DOS será aberto e irá exibir o código de acesso e o valor total de todas as notas, que foram consumidas da API disponibilzada pela Arquivei, e que foram inseridas na base de dados.


# Consumindo API para consultar o valor de uma NFe

## Descrição:
Trata-se de uma API restful exposta através do método GET que necessita receber uma chave de acesso para retornar o valor total da nota fiscal eletronica.

## Como Utilizar:
1. Em [release](https://github.com/rrpatreze/arquivei/releases), fazer o download do arquivo *BuscarValorTotalNota.zip*;
2. Descompactar o arquivo em um diretório local;
3. Executar o arquivo *DesafioArquivei.exe* para subir a API no ar
4. Abrir um browser e acessar a URL fornecida

Request
- Get: /api/receiver/{id}

- Exemplo: http://localhost:5000/api/receiver/35140330290824000104550010003715421390782397

## Resultado Esperado:

- Caso seja encontrado o código de acesso na base de dados:  
*Nota: "35140330290824000104550010003715421390782397"*  
*Valor: 365.89*

- Caso não seja encontrado o código de acesso na base dados:  
*Nota: "xyz" nao encontrada*
