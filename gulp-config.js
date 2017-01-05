module.exports = () => {
    const root = "E:\\Projects";
    const sandbox = root + "\\Instances\\blog.local";

    const config = {
        solutionPath: root + "\\epam-blog-accl",
        website: "http://popsicle.local",
        websiteRoot: sandbox + "\\Website",
        sitecoreLibraries: sandbox + "\\Website\\bin",
        licensePath: sandbox + "\\Data\\license.xml",
        solutionName: "Sitecore.Feature.Blog",
        buildConfiguration: "Debug",
        runCleanBuilds: false
    };

    return config;
}