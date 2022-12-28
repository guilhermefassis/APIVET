# API .NET
Neste repositório encontra-se a criação de uma api feita na semana de desafio API.

Foram realizados planning para levantar o que seria feito e como seria feito.

## Funcionalidades

 Animals Controller
 => GetALL(); * 
 => GetById(int Id); * 
 => GetByIdentificationCode(string code); * 
 => GetByTutorId(int TutorId); * 
 => GetByTutorSSN(string SSN); * 
 => CreateAnimal(CreateAnimalDTO animal); * 
 => CreateAnimalWithTutor(CreateAnimalDTO animal);
 => UpdateAnimal(int id, UpdateAnimalDto anima); *
 => DeleteAnimal(int id);*
 
 Tutor Controller
 => GetALL();*
 => GetById(int id);*
 => GetBySSN(string SSN);*
 => GetByAnimalId(int AnimalId);*
 => GetByAnimalIdentificationCode(string code);*
 => CreateTutor(CreateTutorDto tutor);*
 => CreateTutorWithAddress(CreateTutorDto tutor);*
 => CreateOrReturn(CreateTutorDto tutor); *
 => UpdateTutor(int id, UpdateTutorDto tutor);
 => DeleteTutor(int id);*
 
 Veterinarian Controller
 => GetAll();*
 => GetById(int id);*
 => GetBySSN(string SSN); * 
 => GetBySpecialty(Specialty specialty);*
 => CreateVeterinarian(CreateVeterinarianDto vet);*
 => UpdateVeterinarian(int id, UpdateVeterinarianDto vet);*
 => DeleteVeterinarian(int id);*
 
 Query Controller
 => GetAll(); *
 => GetById(int id);*
 => GetByVeterinarianCRVM(int CRVM); *
 => GetByVeterinarianId(int id);*
 => GetByTutorSSN(string SSN);*
 => GetByTutorId(int TutorId);*
 => GetByAnimalId(int AnimalId);*
 => GetByAnimalIdentificationCode(string code);*
 => GetByDate(Datetime Date);
 => CreateQuery(CreateQueryDto query);*
 => UpdateQuery(int id, UpdateQueryDto query);
 => DeleteQuery(int id);

## Tecnologias utilizadas
- `Entitiy Framework`
- `.NET 5`
- `Xunit`
- `Auto Mapper`
- `MySql`
- `Identity`

## Banco de dados

Há um primeiro usuario cadastrado que tem todas as funcionalidades da api liberada, segue os dados para login:
- Acesso Administrador:
    Usuário: admin@vet.com
    senha: Admin@123

## Como usar o sistema

1° git clone https://git.gft.com/gefi/projeto-api-vet.git

após clonado entre na pasta que clonou com o cmd usando o comando cd

2° adiciona e popule o banco de dados com dotnet ef database update

3° use o dotnet watch run e se divirta

# Atenção

Foram usados modelos de enums dentro da api, na parte de endereço temos os enum publicplace que e o logradouro do endereço, assim atenção ao mandar a string para a converção nao ser mal sucedida e retornar um codigo 400, bad request.

segue os modelos dos enums

    public enum PublicPlace
    {
        street,
        garden,
        avenue,
        park,
        lane
    }
    envie 0 para o sexo feminino e 1 para o masculino

    public enum Sex
    {
        Female,
        Male
    }

    Envie de 0 a 3 para definir a especialidade do veterinario

    public enum Specialty
    {
        surgeon, // Cirurgião
        anesthetist, // Anestesista
        general, // Clinico Geral
        physiotherapist // Fisioterapeuta
    }

## Swagger

Está implementado o Swagger na aplicação, 100% funcional. Caso for alternar entre postaman e swagger e indicado que faça o logout.

A mais detalhes sobre os metodos dentro da documentação do swagger
