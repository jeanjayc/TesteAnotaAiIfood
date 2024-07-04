API de Gerenciamento de Catálogo de Produtos

Bem-vindo à API de Gerenciamento de Catálogo de Produtos! Este projeto faz parte de um teste para a posição de Analista Backend. O objetivo é desenvolver uma API usando .NET para um sistema de gerenciamento de catálogo de produtos em uma aplicação de marketplace.
Índice

    Desafio
    Tecnologias Utilizadas
    Instalação
    Uso
    Endpoints da API
    Contribuição
    Licença

Desafio

A tarefa é desenvolver uma API que permite aos usuários gerenciar um catálogo de produtos. As seguintes histórias de usuário foram convertidas em rotas para a aplicação:

    Registrar um Produto: Usuários podem registrar um produto com seus detalhes (título, descrição, preço, categoria, ID do proprietário).
    Registrar uma Categoria: Usuários podem registrar uma categoria com seus detalhes (título, descrição, ID do proprietário).
    Associar um Produto a uma Categoria: Usuários podem associar um produto a uma categoria.
    Atualizar Produto/Categoria: Usuários podem atualizar os detalhes de um produto ou categoria.
    Excluir Produto/Categoria: Usuários podem excluir um produto ou categoria do seu catálogo.
    Associação Única de Categoria: Um produto pode ser associado a apenas uma categoria por vez.
    Associação Única de Proprietário: Produtos e categorias pertencem a apenas um proprietário.
    Alta Disponibilidade: A API suporta múltiplas solicitações para editar itens/categorias por segundo e fornece um endpoint para busca no catálogo sem buscar no banco de dados.
    Notificações de Mudança no Catálogo: Mudanças no catálogo de produtos são publicadas no tópico "catalog-emit" no AWS SQS.
    Implementação de Consumidor: Um consumidor escuta mudanças no catálogo para um proprietário específico, gera o JSON do catálogo e publica no bucket AWS S3.

Tecnologias Utilizadas

    .NET: Ambiente de desenvolvimento backend
    MongoDB: Banco de dados NoSQL
    AWS SQS: Serviço para notificações de mudanças no catálogo
    AWS S3: Serviço para armazenar o JSON do catálogo
