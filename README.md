# Consumindo API Arquivei para buscar as NFes

Para Windows: executar Receiver.exe que esta dentro do arquivo CarregarNotas.zip

Trata-se de uma aplicação do tipo Console que irá consumir a API disponibilizada pela Arquivei e salvar a chave de acesso e valor total das notas fiscais eletronicas em uma base de dados SQL Server.


# Consumindo API Patreze para consultar o valor de uma NFe

Request
- Get: /api/receiver/{id}

- Exemplo: api/receiver/35140330290824000104550010003715421390782397

Trata-se de uma API restful exposta através do método GET que necessita receber uma chave de acesso para retornar o valor total da nota fiscal eletronica.
