using Microsoft.AspNetCore.Http;

public class BDokumentumFeltoltes{
    public string? Leiras { get; set; }

    public string Fajlnev { get; set; }

    public IFormFile Fajl { get; set; }
}

public class BDokumentumModositas{
    public int DokumentumId{get;set;}
    public string? Leiras { get; set; }

    public string Fajlnev { get; set; }

    public IFormFile? Fajl { get; set; }
}