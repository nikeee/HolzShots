type SemanticVersion = string;
type Url = string;
type UrlTemplate = string;
type RegexPattern = string;
type JsonPath = string;
type XPath = string;
type HttpMethod = "POST" | "PUT";
type UserAgent = string;
type ParamsMap = { [index: string]: string | null };

// TODO: Consider Basic Auth?
// TODO: Allow every header to be set

interface CustomUploader {
    schemaVersion: "0.1.0";
    info: Info;
    uploader: UploaderConfig;
}

interface Info {
    version: SemanticVersion;
    name: string;
    author?: string;
    contact?: string;
    bugsUrl?: Url;
    updateUrl?: Url;
    url?: Url;
    description?: string;
}

interface UploaderConfig {
    fileFormName: string;
    parser: Parser;
    requestUrl: Url;
    method?: HttpMethod;
    headers?: UploaderHeaders;
    postParams?: ParamsMap;
    maxFileSize?: number;
    fileName?: string;
}

interface UploaderHeaders {
    userAgent?: UserAgent;
    referer: string;
}

type Parser = RegExParser | JsonParser | XmlParser;
interface RegExParser {
    kind: "regex";
    url: UrlTemplate;
    success: RegexPattern;
    failure?: RegexPattern;
}
interface JsonParser {
    kind: "json";
    url: UrlTemplate;
    success: JsonPath;
    failure?: JsonPath;
}
interface XmlParser {
    kind: "xml";
    url: UrlTemplate;
    success: XPath;
    failure?: XPath;
}
