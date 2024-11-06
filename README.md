# Inoa
Desenvolvimento de um sistema para a empresa INOA, cujo objetivo é notificar sobre a queda ou aumento do preço de uma ação, considerando limites superiores e inferiores.

***********
<h3>Implementação</h3>
Implementar esse sistema foi um desafio para mim, tendo em mente que não tinha nenhuma experiência prévia com C#, mas como sua prioridade nas linguagens era alta e eu estava disposto a enfrentar mais um desafio. Considerando que basta saber lógica de programação e ter dedicação para conseguir aprender qualquer linguagem, (o fato de eu saber POO me ajudou também) decidi avançar com a escolha. Busquei amigos que cursam economia, aprendi com eles sobre média móvel, como forma de entender melhor o funcionamento e desenvolver alguma feature extra relacionada a isso. 

Inicialmente me atentei com duas importantes questões antes de iniciar o projeto, qual arquitetura e qual design pattern eu utilizaria.
Optei por usar uma arquitetura próxima a arquitetura por camadas e mirei direto no design pattern observer, já que sabia que ele é intimamente ligado com o envio de notificações.

Desenhei o projeto e foquei em cumprir as features principais, já que eu não tinha muito tempo em relação ao tanto que sabia da linguagem.

<h3>Fluxo</h3>
O fluxo que projetei foi: Receber a entrada dos dados, fazer o request para API, tratar os dados recebidos como resposta, implementar o padrão observer, fazer o envio de emails, consolidar tudo isso de forma a obter consistência e caso sobrasse tempo, desenvolver alguma feature extra.

Busquei uma API que fosse gratis e me servisse, então fiz uso da API da brapi: https://brapi.dev e utilizei o maileroo: https://maileroo.com/ como SMTP para envio dos emails.

Me dediquei muito a estudar C#, entender sua estrutura para aplicações console, entender padrões de nomenclatura de variáveis, classes, métodos..., e cumprir as funcionalidades básicas, buscando, também deixar tudo modular.

<h3>Organização</h3>
Organização do sistema:
Dividi o sistema em: Managers, Models, Services, Utils e Observer.
Managers: responsável por processar, chamar o service que faz a requisição para a API.
Services: um deles é responsável por fazer o request e tratar os dados recebidos da API e outro é responsável por fazer o envio do email.
Utils: define uma base para a minha requisição HTTP, de modo que fique mais modular, não seja necessário fazer toda a estrutura sempre para cada requisição.
Observer: implementa o design patter observer, minha interface para o meu subject, notificação para o envio, adição de emails para cada observer e coloca o preço da ação para cada observer.
Models: para tratamento dos dados recebidos da API.

<h3>Detalhamento</h3>
Recebo os argumentos e trato cada um deles -> Pego a lista de emails e guardo em um vetor -> instancio minhas duas classes para o alerta dos emails -> faço o esquema para que o terminal continue fazendo as requisições e finalize apenas quando solicitado -> Faço a chamada do método GetQuoteAsync para requisição da api -> Esse método chama meu service para que a requisição seja feita e os dados sejam tratados -> Com os dados da ação chamo UpdateStockPrice que atualiza o preço da ação e inicia o processo de notificação dos emails -> meu Subject (StockMarketNotification) notifica seus observers (EmailAlert) caso o preço da ação esteja acima do teto desejado, ou o preço da ação esteja abaixo do piso desejado -> Atribui as configurações de Smtp e faz o envio dos emails com o método Send da classe SendEmail.

<h3>Dificuldades</h3>
<ul>
<li>Escolha da API, por incrível que pareça, essa missão foi mais dificil do que pensava, tinham algumas APIs com documentações confusas, muitas cobravam pelo serviço e poucas ofereciam o princípal que eu buscava, o preço da ação.</li>
<li>Request para API, como foi uma das minhas primeiras etapas, creio que foi a que tive mais dificuldade, até me acostumar com a linguagem, entender erros que estava cometendo, principalmente na hora de tratar os dados, consumiu boa parte do meu tempo.</li>
<li>Organização da estrutura, como eu estava muito acostumado com o padrão MVC e ele não me era ideal para essa aplicação console, fiquei um pouco confuso quanto a organização.</li>
</ul>

<h3>Referências</h3>
Alguns dos sites usados como base para o desenvolvimento: https://refactoring.guru/pt-br/design-patterns/csharp, https://learn.microsoft.com/pt-br/dotnet/csharp/tour-of-csharp/overview https://learn.microsoft.com/pt-br/dotnet/core/tools/?tabs=netcore2x https://learn.microsoft.com/pt-br/visualstudio/get-started/csharp/tutorial-console?view=vs-2022 https://learn.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client https://medium.com/@tajbiulrawol/understanding-the-observer-pattern-enhancing-notifications-in-c-applications-ace780de166b https://maileroo.com/blog/send-emails-in-c-net-via-smtp/


