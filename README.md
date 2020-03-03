# ASP.NET 

### **Core 3.1**

**Recursos:**

* Várias melhorias para Blazor.

**Referências:**

* https://docs.microsoft.com/pt-br/aspnet/core/release-notes/aspnetcore-3.1

---

### **Core 3.0**

**Recursos:**

* Blazor.
* gRPC (Remote Procedure Call - Chamada de Procedimento Remoto).
* Melhorias no SignalR.
* Novo padrão de serialização JSON nativo, otimizado para texto UTF-8, que entra no lugar do Newtonsoft.JSON.
* Novas diretivas do Razor.
* Host genérico (HostBuilder) agora substitui o WebHostBuilder. Isso facilita a disponibilização de aplicações asp.net core em hosts mais diversos, e não só em web.
* EventCounters.
* Melhorias no motor de roteamento. Agora você consegue configurar cada ponto de extremidade.
* Verificações de integridade (HealthCheck).
* Relatório de erros aprimorado no IIS.
* Novo tipo de template para Worker Service (tarefas em segundo plano). Esse tipo de aplicação pode ser usado para executar tarefas de longa duração ou "pesadas" em segundo plano.
* Melhorias de desempenho, que reduzem o uso de memória e melhoram a taxa de transferência.

**Referências:**

* https://docs.microsoft.com/pt-br/aspnet/core/release-notes/aspnetcore-3.0
* https://github.com/aspnet/Announcements/issues/325

---

### **Core 2.2** 

**Recursos:** 

* Ferramentas e experiências de runtime aprimoradas para a criação de documentos do OpenAPI (também conhecido como Swagger).
* Novo motor de roteamento MVC.
* Novo serviço de verificações de integridade (HealthCheck). Esses serviços são usados por orquestradores de conteineres ou balanceadores de carga.
* Suporte ao HTTP/2.
* Agora o aplicativo pode ser hospedado como processo do IIS. Antes o aplicativo ficava atrás do IIS que funcionava como um proxy reverso. Agora a hospedagem é "dentro" do IIS e não "fora".
* Melhorias no Middleware do CORS.
* Compactação de resposta no formato Brotli.

**Referências:**  
* https://docs.microsoft.com/pt-br/aspnet/core/release-notes/aspnetcore-2.2 
* https://github.com/dotnet/aspnetcore/releases/tag/2.2.0 

--- 

### **Core 2.1** 

**Recursos:** 

* Suporte nativo a HTTPS e HSTS.
* Compartibilidade de versão.
* Novo metapackage *Microsoft.AspNetCore.App* em substituição de *Microsoft.AspNetCore.All* reduz o número de dependências de terceiros.
* HttpClientFactory: permite a pré configuração de clientes http para uso posterior sem ter que ficar configurando ao usar.
* Em ApiController ao existir problemas de validação vai gerar automaticamente retorno tipo 400.
* Em ApiController não precisa mais informar [FromBody] para tipos complexos. Quando se tratar de tipos complexos ocorrerá inferência de tipo.
* Retorno *ActionResult\<T\>* permite retornar objeto do tipo T ou NotFound. 
* Suporte a imagens Docker **Alpine** (bem mais leves).
* Identity como biblioteca. 
* Hosting em tempo de processo. 
* WebHooks.
* SignalR.

**Referências:**  
* https://www.youtube.com/watch?v=g7wMjigxS-s  

--- 

### **Core 2.0** 

**Recursos:** 

* Metapackage Microsoft.AspNetCore.All para facilitar a criação e manutenção de aplicações.
* IConfiguration no lugar de ConfigurationBuilder.
* ILoggerFactory não é passado por padrão para o método Configure.
* Com o .NET Standard 2.0 já é possível usar recursos do .NET Full Framework que estavam ausentes até então (Datasets, Serialização Binária, Reflection e outros).
* Razor Pages: páginas simples ou views sem controllers.
* A forma de habilitar autenticação foi alterada. Primeiro você adiciona em **ConfigureServices** o tipo de autenticação e a configuração pertinente e depois em **Configure** você inclui a autenticação no pipeline através do método **UseAuthentication()**.

**Referências:**  
* https://medium.com/@renato.groffe/novidades-do-asp-net-core-2-0-c012129b5841 
* http://www.macoratti.net/17/06/aspncore2_1.htm 
* https://www.infoq.com/br/presentations/aspnet-core-2-e-dotnet-standard-2


---

**Fontes** 

* https://docs.microsoft.com/pt-br/aspnet/core/
* https://github.com/aspnet/Home
* https://github.com/aspnet/Home/wiki/Roadmap
* https://docs.microsoft.com/en-us/aspnet/index 
* https://www.youtube.com/channel/UCIahKJr2Q50Sprk5ztPGnVg (Canal .NET) 
* https://www.lambda3.com.br/tag/asp-net-core/ 
* https://automapper.org/
* http://www.eduardopires.net.br/2013/08/asp-net-mvc-utilizando-automapper-com-view-model-pattern/
* https://dotnetcoretutorials.com/2017/09/23/using-automapper-asp-net-core/
* https://dotnetthoughts.net/using-automapper-in-aspnet-core-project/
* http://aspnetcorepath.com/automapper-in-asp-net-core-2/
* https://dotnetcoretutorials.com/2017/09/23/using-automapper-asp-net-cor
* https://docs.microsoft.com/pt-br/aspnet/core/mvc/models/validation?view=aspnetcore-2.1
* https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/adding-validation
* https://cecilphillip.com/fluent-validation-rules-with-asp-net-core/    
* https://docs.microsoft.com/pt-br/aspnet/mvc/overview/older-versions-1/controllers-and-routing/understanding-action-filters-cs
* https://docs.microsoft.com/pt-br/aspnet/web-api/overview/security/authentication-filters
* https://www.c-sharpcorner.com/article/filters-in-Asp-Net-mvc-5-0-part-twelve/
* http://talenttuner.com/Blogs/MVC5/understanding-asp-net-mvc-filters/
* http://parmezani.net/asp-net-mvc-5-classificacaofiltragem-e-paginacao/    
* http://www.macoratti.net/15/10/mvc_authf1.htm
* https://imasters.com.br/dotnet/fluxo-de-requisicoes-e-filtros-no-net-core    
* http://www.macoratti.net/15/05/asp_ident1.htm
* https://docs.microsoft.com/pt-br/aspnet/identity/overview/getting-started/introduction-to-aspnet-identity
* http://www.eduardopires.net.br/2014/03/asp-net-identity-customizando-cadastro-usuarios/    
* https://www.youtube.com/watch?v=qg6ef8ggXPU
* https://docs.microsoft.com/pt-br/aspnet/mvc/overview/security/create-an-aspnet-mvc-5-web-app-with-email-confirmation-and-password-reset
* https://www.devmedia.com.br/asp-net-identity-como-trabalhar-com-owin/33003
* https://www.youtube.com/watch?v=LCQdTQoDMas (autenticação por cookie)
* https://medium.com/@renato.groffe/asp-net-core-2-0-jwt-identity-core-na-autentica%C3%A7%C3%A3o-de-apis-e2a6fab07421        
* https://docs.microsoft.com/pt-br/dotnet/standard/microservices-architecture/secure-net-microservices-web-applications/
* https://imasters.com.br/apis-microsservicos/asp-net-web-api-implementando-seguranca-via-tokens-parte-01
* https://medium.com/@renato.groffe/net-core-2-0-jwt-consumindo-uma-api-que-utiliza-tokens-414f32f670de
* https://medium.com/@fulviocanducci/autentica%C3%A7%C3%A3o-web-api-core-com-jwtbearer-2c1e9e3dade4
* https://msdn.microsoft.com/pt-br/library/dn376307.aspx
* https://www.youtube.com/watch?v=-AOyPxVwheE
* https://medium.com/tableless/autoriza%C3%A7%C3%A3o-em-apis-asp-net-core-4f3648f5c28a    