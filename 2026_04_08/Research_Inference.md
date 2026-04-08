# AI Inference – Research

## Was ist Inference?

Inference (dt. *Inferenz*) bezeichnet den Vorgang, bei dem ein bereits trainiertes KI-Modell eine Eingabe verarbeitet und eine Ausgabe erzeugt – also wenn das Modell „benutzt" wird (z. B. eine Frage beantwortet, ein Bild erkennt oder Code generiert).

> **Kurz gesagt:** Training = das Modell lernt. Inference = das Modell denkt.

Inference ist rechenintensiv und erfordert je nach Modellgröße enorm viel GPU-Leistung, weshalb Kosten und Verfügbarkeit ein zentrales Thema sind.

---

## 1. Wo bekomme ich gratis Inference?

| Anbieter | Modell(e) | Limit / Bedingung |
|---|---|---|
| **Google AI Studio** | Gemini 2.0 Flash, Gemini 1.5 Pro | Gratis-Tier mit API-Key |
| **Groq** | LLaMA 3, Mistral, Gemma | Kostenloses Free-Tier (Rate-Limits) |
| **Hugging Face Inference API** | Tausende Open-Source-Modelle | Gratis mit Account, langsamer als paid |
| **OpenRouter** (free models) | Diverse Open-Source-Modelle | Modelle mit `:free`-Tag gratis |
| **Cloudflare Workers AI** | LLaMA, Mistral u. a. | Gratis-Kontingent im Free-Plan |
| **Ollama (lokal)** | Beliebige Open-Source-Modelle | 100 % gratis, läuft auf eigenem PC |
| **Mistral API** | Mistral 7B | Gratis-Tier verfügbar |

> Mit **Ollama** kann man Modelle wie LLaMA 3 oder Mistral direkt auf dem eigenen Rechner laufen lassen – komplett kostenlos, keine API nötig.

---

## 2. Wo bekomme ich günstige Inference?

| Anbieter | Modell(e) | Preis (ca.) |
|---|---|---|
| **Groq** | LLaMA 3.3 70B | ~$0.59 / 1M Tokens |
| **Together AI** | LLaMA 3, Mistral, FLUX | Ab ~$0.10 / 1M Tokens |
| **OpenRouter** | Viele Modelle aggregiert | Preisvergleich über alle Anbieter |
| **Fireworks AI** | LLaMA 3, Mixtral | Ab ~$0.20 / 1M Tokens |
| **DeepSeek API** | DeepSeek V3, R1 | Sehr günstig, ~$0.27 / 1M Tokens |
| **Cerebras** | LLaMA 3.1 70B | ~$0.60 / 1M Tokens, sehr schnell |
| **Anthropic (Claude Haiku)** | Claude Haiku 3.5 | ~$0.80 / 1M Tokens |
| **OpenAI (GPT-4o mini)** | GPT-4o mini | ~$0.15 / 1M Input-Tokens |

> Für Preisvergleiche lohnt sich **[openrouter.ai](https://openrouter.ai)** – dort sind viele Anbieter auf einen Blick vergleichbar.

---

*Recherche erstellt für den Programmierunterricht – HTL Spengergasse, 3AHWII*
