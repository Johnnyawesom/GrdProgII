# Text Crawler
- _Author: Mads Søndergaard_
- _Date: 26/05/2021_
- _Client: Semiotic Research Group

### Goals:
A program that is capable of parsing a given text in accordance with various predetermined rules, in order
to analyse the text for certain incongruities, such as sequentially repeating words (e.g. "that that"),
as well as atomizing it for semiotic statistical analysis (e.g. occurence of each letter).

Or, in short form:
- Parse text by sentence (excluding subsentences), i.e. at every "."
- Parse text by sentence and subsentence, i.e. at every "." and ","
- Finding repeated words that aren't divided by , or .
- Statistical analysis, initially in terms of letter occurence (e.g. A appears 42 times in this text)

#### Input:
- string iText = Input text to be processed
- string[] sect = array for holding the subdivided (section) contents of iText
- string[] sent = array for holding the subdivided (sentence) contents of iText
- string sPath = Path to save the results to
- string lPath = Path to the text to load

#### Things to consider:
- Validate path (try..catch)
- Iterate on targets (loops)
- Convert string to and from string arrays
- Divide each operation into its own separate class if possible.